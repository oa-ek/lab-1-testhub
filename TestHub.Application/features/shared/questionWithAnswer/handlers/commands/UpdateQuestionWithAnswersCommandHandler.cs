using Application.features.shared.questionwithanswer.requests.commands;

namespace Application.features.shared.questionwithanswer.handlers.commands;

public class UpdateQuestionWithAnswersCommandHandler : IRequestHandler<UpdateQuestionWithAnswersCommand, BaseCommandResponse<RespondQuestionDto>>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IAnswerRepository _answerRepository;
    private readonly IMapper _mapper;

    public UpdateQuestionWithAnswersCommandHandler(IQuestionRepository questionRepository, IMapper mapper, IAnswerRepository answerRepository)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
        _answerRepository = answerRepository;
    }

    public async Task<BaseCommandResponse<RespondQuestionDto>> Handle(UpdateQuestionWithAnswersCommand command, CancellationToken cancellationToken)
    {
        var requestQuestion = command.QuestionWithAnswerDto;
        if (requestQuestion == null)
            return new BadRequestFailedStatusResponse<RespondQuestionDto>(new List<ValidationFailure>
            {
                new ("QuestionDto", "QuestionDto cannot be null.")
            });
        
        var questionDto = _mapper.Map<RequestQuestionDto>(requestQuestion);
        var validationResult = await ValidateQuestionAsync(questionDto, cancellationToken);
        if (!validationResult.IsValid) return new BadRequestFailedStatusResponse<RespondQuestionDto>(validationResult.Errors);
        
        var question = await _questionRepository.Get(command.QuestionId);
        if (question == null) return new NotFoundFailedStatusResponse<RespondQuestionDto>(command.QuestionId);
        
        _mapper.Map(questionDto, question);
        await _questionRepository.Update(question);

        var requestAnswers = requestQuestion.AnswerDtos;
        if (requestAnswers == null)
            return new BadRequestFailedStatusResponse<RespondQuestionDto>(new List<ValidationFailure>
            {
                new ("AnswerDtos", "AnswerDtos cannot be null.")
            });

        await ProcessAnswersAsync(question.Id, requestAnswers, cancellationToken);

        return new OkSuccessStatusResponse<RespondQuestionDto>(question.Id);
    }

    private async Task<ValidationResult> ValidateQuestionAsync(RequestQuestionDto questionDto, CancellationToken cancellationToken)
    {
        var validator = new RequestQuestionDtoValidator(_questionRepository);
        return await validator.ValidateAsync(questionDto, cancellationToken);
    }

    private async Task ProcessAnswersAsync(int questionId, IEnumerable<RequestAnswerDto> requestAnswers, CancellationToken cancellationToken)
    {
        var getAnswersRequest = await _answerRepository.GetByQuestion(questionId);
        if (getAnswersRequest.Any())
            await _answerRepository.DeleteRange(getAnswersRequest);
        
        foreach (var requestAnswerDto in requestAnswers)
        {
            var answerValidationResult = await ValidateAnswerAsync(requestAnswerDto, cancellationToken);
            if (!answerValidationResult.IsValid)
                return;
            
            var answer = _mapper.Map<Answer>(requestAnswerDto);
            answer.QuestionId = questionId;

            await _answerRepository.Add(answer);
        }
    }
    
    private async Task<ValidationResult> ValidateAnswerAsync(RequestAnswerDto answerDto, CancellationToken cancellationToken)
    {
        var answerDtoValidator = new RequestAnswerDtoValidator(_answerRepository);
        return await answerDtoValidator.ValidateAsync(answerDto, cancellationToken);
    }
}