using Application.results.common;

namespace Application.features.test.requests.queries;

public class GetTestDetailedListRequestByUser : IRequest<BaseCommandResult<List<RespondTestDto>>>
{
    public required int OwnerId { get; set; }
}