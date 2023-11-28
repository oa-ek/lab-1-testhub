namespace Application.features.question.requests.queries;

public class GetQuestionDetailedListRequestByTest : IRequest<BaseCommandResponse<List<RespondQuestionDto>>>
{
    public required int TestId { get; set; }
}