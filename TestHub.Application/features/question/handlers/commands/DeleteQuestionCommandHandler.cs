using Application.features.question.requests.commands;
using Application.results.common;

namespace Application.features.question.handlers.commands;

public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, BaseCommandResult<RespondQuestionDto>>
{
    private readonly IQuestionRepository _repository;

    public DeleteQuestionCommandHandler(IQuestionRepository repository)
    {
        _repository = repository;
    }
    public async Task<BaseCommandResult<RespondQuestionDto>> Handle(DeleteQuestionCommand command, CancellationToken cancellationToken)
    {
        var question = await _repository.Get(command.Id);
        if (question == null) return new NotFoundFailedStatusResult<RespondQuestionDto>(command.Id);

        await _repository.Delete(question);
        return new OkSuccessStatusResult<RespondQuestionDto>(command.Id);
    }
}