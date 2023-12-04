namespace Application.contracts.authentication;

public record LoginRequest(
    string Email,
    string Password
);