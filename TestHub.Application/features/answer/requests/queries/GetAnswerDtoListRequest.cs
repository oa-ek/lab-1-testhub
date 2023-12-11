using Application.results.common;

namespace Application.features.answer.requests.queries;

public class GetAnswerDtoListRequest : IRequest<BaseCommandResult<List<RespondAnswerDto>>> {}