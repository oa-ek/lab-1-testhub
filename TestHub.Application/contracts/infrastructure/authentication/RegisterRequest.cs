namespace Application.contracts.infrastructure.authentication;

public record RegisterRequest (
    string UserName,
    string Email,
    string Password
);