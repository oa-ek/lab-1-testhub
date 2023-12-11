using Application.results.common;

namespace Application.contracts.persistence.authentication;

public interface IAuthenticationService
{
    Task<BaseCommandResult<RespondAuthenticationDto>> Login(RequestLoginDto? request);
    Task<BaseCommandResult<RespondAuthenticationDto>> Register(RequestRegisterDto? request);
}