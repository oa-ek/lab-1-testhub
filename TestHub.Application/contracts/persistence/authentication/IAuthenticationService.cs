namespace Application.contracts.persistence.authentication;

public interface IAuthenticationService
{
    Task<BaseCommandResponse<RespondAuthenticationDto>> Login(RequestLoginDto? request);
    Task<BaseCommandResponse<RespondAuthenticationDto>> Register(RequestRegisterDto? request);
}