using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.models.identity;
using Application.responses.success;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ValidationFailure = FluentValidation.Results.ValidationFailure;

namespace TestHub.Infrastructure.services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _repository;

    public AuthService(IUserRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    public async Task<BaseCommandResponse<AuthResponse>> Login(AuthRequest? request)
    {
        var user = await _repository.GetByEmail(request.Email);
        if (user == null)
            return new NotFoundFailedStatusResponse<AuthResponse>(request.Email);
        
        if (user.IsEmailVerified ==false )
            return new BadRequestFailedStatusResponse<AuthResponse>(new List<ValidationFailure>
            {
                 new ("AuthResponse", "The email is not verified")
            });
        
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            return new BadRequestFailedStatusResponse<AuthResponse>(new List<ValidationFailure>
            {
                new ("AuthResponse", "Wrong password.")
            });
        
        var token = CreateToken(user);
        var response = new AuthResponse
        {
            Email = user.Email,
            UserName = user.Name,
            Token = new JwtSecurityTokenHandler().WriteToken(token)
        };
        return new OkSuccessStatusResponse<AuthResponse>(response);
    }

    public async Task<BaseCommandResponse<RegistrationResponse>> Register(RegistrationRequest request)
    {
        throw new NotImplementedException();
    }
    
    private SecurityToken CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim("Name", user.Name),
            new Claim("Email", user.Email),
            new Claim("Role", user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AuthSettings:Token").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        return new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );
    }
    
    private string CreateRandomToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }
}