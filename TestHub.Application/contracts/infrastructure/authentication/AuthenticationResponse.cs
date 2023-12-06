namespace Application.contracts.infrastructure.authentication;

public record AuthenticationResponse(
    int Id,
    string UserName,
    string Email,
    string Token
);