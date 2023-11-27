using Application.features.question.requests.commands;

namespace Application.features.question.handlers.commands;

public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, BaseCommandResponse<RespondQuestionDto>>
{
    private readonly IQuestionRepository _repository;
    private readonly IMapper _mapper;

    public CreateQuestionCommandHandler(IQuestionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<BaseCommandResponse<RespondQuestionDto>> Handle(CreateQuestionCommand command, CancellationToken cancellationToken)
    {
        var validator = new RequestQuestionDtoValidator(_repository);
        var validationResult = await validator.ValidateAsync(command.QuestionDto, cancellationToken);
        if (!validationResult.IsValid) return new BadRequestFailedStatusResponse<RespondQuestionDto>(validationResult.Errors);
        
        var question = _mapper.Map<Question>(command.QuestionDto);

        question = await _repository.Add(question);
        return new CreatedSuccessStatusResponse<RespondQuestionDto>(question.Id);
    }
}