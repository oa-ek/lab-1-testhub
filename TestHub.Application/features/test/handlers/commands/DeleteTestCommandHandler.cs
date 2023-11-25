using Application.features.test.requests.commands;
using Application.persistence.contracts;

namespace Application.features.test.handlers.commands;

public class DeleteTestCommandHandler : IRequestHandler<DeleteTestCommand, Unit>
{
    private readonly ITestRepository _repository;

    public DeleteTestCommandHandler(ITestRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Unit> Handle(DeleteTestCommand command, CancellationToken cancellationToken)
    {
        var test = await _repository.Get(command.Id);

        await _repository.Delete(test);
        return Unit.Value;
    }
}