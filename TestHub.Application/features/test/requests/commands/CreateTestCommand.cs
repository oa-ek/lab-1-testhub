using Application.dtos.requestsDto;

namespace Application.features.test.requests.commands;

public class CreateTestCommand : IRequest<BaseCommandResponse>
{
    public required RequestTestDto TestDto { get; set; }
}