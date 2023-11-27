namespace Application.features.test.requests.queries;

public class GetTestDetailedListRequestByUser : IRequest<BaseCommandResponse<List<RespondTestDto>>>
{
    public required int OwnerId { get; set; }
}