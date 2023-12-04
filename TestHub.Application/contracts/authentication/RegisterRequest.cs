namespace Application.contracts.authentication;

public record RegisterRequest (
    string UserName,
    string Email,
    string Password
);