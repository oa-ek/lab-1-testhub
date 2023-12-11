using Application.results.common;

namespace Application.features.question.requests.queries;

public class GetQuestionDetailedListRequest  : IRequest<BaseCommandResult<List<RespondQuestionDto>>> {}