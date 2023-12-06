namespace Application.contracts.infrastructure.authentication;

public record LoginRequest(
    string Email,
    string Password
);