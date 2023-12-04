namespace Application.contracts.persistence.authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(string name, string email, string role);
}