using Application.features.question.requests.commands;

namespace Application.features.question.handlers.commands;

public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, BaseCommandResponse<RespondQuestionDto>>
{
    private readonly IQuestionRepository _repository;
    private readonly IMapper _mapper;

    public UpdateQuestionCommandHandler(IQuestionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<BaseCommandResponse<RespondQuestionDto>> Handle(UpdateQuestionCommand command, CancellationToken cancellationToken)
    {
        var validator = new RequestQuestionDtoValidator(_repository);
        var validationResult = await validator.ValidateAsync(command.QuestionDto, cancellationToken);
        if (!validationResult.IsValid) return new BadRequestFailedStatusResponse<RespondQuestionDto>(validationResult.Errors);
        
        var question = await _repository.Get(command.Id);
        if (question == null) return new NotFoundFailedStatusResponse<RespondQuestionDto>(command.Id);
        
        _mapper.Map(command.QuestionDto, question);
        await _repository.Update(question);
        return new OkSuccessStatusResponse<RespondQuestionDto>(question.Id);
    }
}