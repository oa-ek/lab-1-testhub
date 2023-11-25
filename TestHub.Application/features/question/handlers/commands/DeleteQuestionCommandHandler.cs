using Application.features.question.requests.commands;
using Application.persistence.contracts;

namespace Application.features.question.handlers.commands;

public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, Unit>
{
    private readonly IQuestionRepository _repository;

    public DeleteQuestionCommandHandler(IQuestionRepository repository)
    {
        _repository = repository;
    }
    public async Task<Unit> Handle(DeleteQuestionCommand command, CancellationToken cancellationToken)
    {
        var question = await _repository.Get(command.Id);

        await _repository.Delete(question);
        return Unit.Value;
    }
}