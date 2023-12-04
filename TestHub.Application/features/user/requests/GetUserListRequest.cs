using Application.dtos.sharedDTOs;

namespace Application.features.user.requests;

public class GetUserListRequest : IRequest<List<RespondUserDto>> {}