using Application.features.user.requests.commands;
using Application.results.common;

namespace Application.features.user.handlers.commands;

public class SetVerifiedEmailRequestHandler : IRequestHandler<SetVerifiedEmailRequest, BaseCommandResult<RespondAuthenticationDto>>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public SetVerifiedEmailRequestHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<BaseCommandResult<RespondAuthenticationDto>> Handle(SetVerifiedEmailRequest command, CancellationToken cancellationToken)
    {
        if (command.VerifiedEmailDto == null)
            return new BadRequestFailedStatusResult<RespondAuthenticationDto>("VerifiedEmailDto cannot be null.");

        var user = await _repository.GetByEmail(command.VerifiedEmailDto.Email);
        if (user == null) return new NotFoundFailedStatusResult<RespondAuthenticationDto>(command.VerifiedEmailDto.Email);

        await _repository.VerifiedEmail(user);
        var respondUserDto = _mapper.Map<RespondUserDto>(user);
        var respondAuthenticationDto = new RespondAuthenticationDto
        {
            RespondUserDto = respondUserDto,
            Token = user.Token
        };
        return new OkSuccessStatusResult<RespondAuthenticationDto>(respondAuthenticationDto);
    }
}