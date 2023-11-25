using Application.features.answer.requests.commands;
using Application.persistence.contracts;

namespace Application.features.answer.handlers.commands;

public class DeleteAnswerCommandHandler : IRequestHandler<DeleteAnswerCommand, Unit>
{
    private readonly IAnswerRepository _repository;

    public DeleteAnswerCommandHandler(IAnswerRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Unit> Handle(DeleteAnswerCommand command, CancellationToken cancellationToken)
    {
        var answer = await _repository.Get(command.Id);

        await _repository.Delete(answer);
        return Unit.Value;
    }
}