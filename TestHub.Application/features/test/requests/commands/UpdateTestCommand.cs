using Application.dtos.requestsDto;

namespace Application.features.test.requests.commands;

public class UpdateTestCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public required RequestTestDto TestDto { get; set; }
}