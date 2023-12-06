using Application.features.user.requests.commands;

namespace Application.features.user.handlers.commands;

public class SetVerifiedEmailRequestHandler : IRequestHandler<SetVerifiedEmailRequest, BaseCommandResponse<RespondAuthenticationDto>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public SetVerifiedEmailRequestHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<BaseCommandResponse<RespondAuthenticationDto>> Handle(SetVerifiedEmailRequest command, CancellationToken cancellationToken)
    {
        if (command.VerifiedEmailDto == null)
            return new BadRequestFailedStatusResponse<RespondAuthenticationDto>("VerifiedEmailDto cannot be null.");

        var user = await _repository.GetByEmail(command.VerifiedEmailDto.Email);
        if (user == null) return new NotFoundFailedStatusResponse<RespondAuthenticationDto>(command.VerifiedEmailDto.Email);

        await _repository.VerifiedEmail(user);
        var respondUserDto = _mapper.Map<RespondUserDto>(user);
        var respondAuthenticationDto = new RespondAuthenticationDto
        {
            RespondUserDto = respondUserDto,
            Token = user.Token
        };
        return new OkSuccessStatusResponse<RespondAuthenticationDto>(respondAuthenticationDto);
    }
}