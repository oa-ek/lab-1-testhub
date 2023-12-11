using Application.results.common;

namespace Application.features.question.requests.queries;

public class GetQuestionDetailedListRequestByTest : IRequest<BaseCommandResult<List<RespondQuestionDto>>>
{
    public required int TestId { get; set; }
}