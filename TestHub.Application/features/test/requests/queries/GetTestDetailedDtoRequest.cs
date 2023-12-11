using Application.results.common;

namespace Application.features.test.requests.queries;

public class GetTestDetailedDtoRequest: IRequest<BaseCommandResult<RespondTestDto>>
{
    public int Id { get; set; }
}