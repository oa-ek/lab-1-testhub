using Application.features.question.requests.commands;

namespace Application.features.question.handlers.commands;

public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, BaseCommandResponse<RespondQuestionDto>>
{
    private readonly IQuestionRepository _repository;

    public DeleteQuestionCommandHandler(IQuestionRepository repository)
    {
        _repository = repository;
    }
    public async Task<BaseCommandResponse<RespondQuestionDto>> Handle(DeleteQuestionCommand command, CancellationToken cancellationToken)
    {
        var question = await _repository.Get(command.Id);
        if (question == null) return new NotFoundFailedStatusResponse<RespondQuestionDto>(command.Id);

        await _repository.Delete(question);
        return new OkSuccessStatusResponse<RespondQuestionDto>(command.Id);
    }
}