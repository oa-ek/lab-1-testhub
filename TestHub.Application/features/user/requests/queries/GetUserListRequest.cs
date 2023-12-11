using Application.results.common;

namespace Application.features.user.requests.queries;

public class GetUserListRequest : IRequest<BaseCommandResult<List<RespondUserDto>>> {}