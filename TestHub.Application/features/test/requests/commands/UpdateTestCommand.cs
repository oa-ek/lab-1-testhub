using Application.dtos.requestsDto;
using Application.results.common;

namespace Application.features.test.requests.commands;

public class UpdateTestCommand : IRequest<BaseCommandResult<RespondTestDto>>
{
    public int Id { get; set; }
    public required RequestTestDto? TestDto { get; set; }
}