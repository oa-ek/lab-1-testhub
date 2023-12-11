using Application.results.common;

namespace Application.features.test.requests.commands;

public class CreateTestCommand : IRequest<BaseCommandResult<RespondTestDto>>
{
    public required RequestTestDto? TestDto { get; set; }
    public required int OwnerId { get; set; }
}