using Application.features.test.requests.commands;
using Application.results.common;

namespace Application.features.test.handlers.commands;

public class DeleteTestCommandHandler : IRequestHandler<DeleteTestCommand, BaseCommandResult<RespondTestDto>>
{
    private readonly ITestRepository _repository;

    public DeleteTestCommandHandler(ITestRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<BaseCommandResult<RespondTestDto>> Handle(DeleteTestCommand command, CancellationToken cancellationToken)
    {
        var test = await _repository.Get(command.Id);
        if (test == null) return new NotFoundFailedStatusResult<RespondTestDto>(command.Id);

        await _repository.Delete(test);
        return new OkSuccessStatusResult<RespondTestDto>(command.Id);
    }
}