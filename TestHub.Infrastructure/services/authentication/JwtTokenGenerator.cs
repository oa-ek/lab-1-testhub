using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace TestHub.Infrastructure.services.authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IOptions<JwtSettings> jwtSettingsOptions)
    {
        _jwtSettings = jwtSettingsOptions.Value;
    }

    public string GenerateToken(string name, string email, string role)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha512Signature);
        var claims = new []
        {
            new Claim("Name", name),
            new Claim("Email", email),
            new Claim("Role", role)
        };

        var securityToken = new JwtSecurityToken(
            issuer:_jwtSettings.Issuer, 
            audience:_jwtSettings.Audience,
            expires: DateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpirationTimeInMinutes),
            claims: claims, 
            signingCredentials:signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}