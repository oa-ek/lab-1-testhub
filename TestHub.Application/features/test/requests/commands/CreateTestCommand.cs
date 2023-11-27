using Application.dtos.requestsDto;

namespace Application.features.test.requests.commands;

public class CreateTestCommand : IRequest<BaseCommandResponse<RespondTestDto>>
{
    public required RequestTestDto? TestDto { get; set; }
}