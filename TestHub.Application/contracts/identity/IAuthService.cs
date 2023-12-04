using Application.models.identity;

namespace Application.contracts.identity;

public interface IAuthService
{
    Task<BaseCommandResponse<AuthResponse>> Login(AuthRequest? request);
    Task<BaseCommandResponse<RegistrationResponse>> Register(RegistrationRequest request);
}