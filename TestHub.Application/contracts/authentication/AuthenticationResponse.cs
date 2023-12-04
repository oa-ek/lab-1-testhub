namespace Application.contracts.authentication;

public record AuthenticationResponse(
    int Id,
    string UserName,
    string Email,
    string Token
);