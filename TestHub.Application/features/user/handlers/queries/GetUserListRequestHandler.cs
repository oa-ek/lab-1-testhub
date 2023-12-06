using Application.features.user.requests.queries;

namespace Application.features.user.handlers.queries;

public class GetUserListRequestHandler : IRequestHandler<GetUserListRequest, BaseCommandResponse<List<RespondUserDto>>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public GetUserListRequestHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<BaseCommandResponse<List<RespondUserDto>>> Handle(GetUserListRequest request, CancellationToken cancellationToken)
    {
        var users = await _repository.GetAll();

        var respondUserDtos = _mapper.Map<List<RespondUserDto>>(users);
        return new OkSuccessStatusResponse<List<RespondUserDto>>(respondUserDtos);
    }
}