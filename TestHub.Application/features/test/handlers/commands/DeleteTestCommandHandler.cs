using Application.features.test.requests.commands;

namespace Application.features.test.handlers.commands;

public class DeleteTestCommandHandler : IRequestHandler<DeleteTestCommand, BaseCommandResponse<RespondTestDto>>
{
    private readonly ITestRepository _repository;

    public DeleteTestCommandHandler(ITestRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<BaseCommandResponse<RespondTestDto>> Handle(DeleteTestCommand command, CancellationToken cancellationToken)
    {
        var test = await _repository.Get(command.Id);
        if (test == null) return new NotFoundFailedStatusResponse<RespondTestDto>(command.Id);

        await _repository.Delete(test);
        return new OkSuccessStatusResponse<RespondTestDto>(command.Id);
    }
}