using Application.dtos.requestsDto;

namespace Application.features.test.requests.commands;

public class CreateTestCommand : IRequest<int>
{
    public RequestTestDto TestDto { get; set; }
}