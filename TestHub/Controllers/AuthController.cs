using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TestHub.Core.Dtos;
using TestHub.Core.Models;
using TestHub.Infrastructure.Services;
using TestHub.Infrastructure.Services.Password;

namespace TestHub.Controllers;

[Route("api/Auth")]
[Produces("application/json")]
[ApiController]
public class AuthController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly UserService _userService;
    private readonly PasswordService _passwordService;
    private readonly AuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IConfiguration configuration, UserService userService, 
        ILogger<AuthController> logger, PasswordService passwordService, AuthService authService)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _passwordService = passwordService ?? throw new ArgumentNullException(nameof(passwordService));
        _authService = authService;
    }

    [HttpPost("register")]
    public ActionResult<User> Register(UserDto? userDto)
    {
        if (userDto == null)
            return StatusCode(StatusCodes.Status400BadRequest, "Invalid object identification.");

        var validationPwdResult = _passwordService.ValidatePassword(userDto.Password);
        if (!validationPwdResult.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, validationPwdResult.ErrorMessage);
        
        var modelValidator = new ModelValidatorService();
        var validationResult = modelValidator.ValidateModel(userDto);

        if (validationResult.IsValid)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            var currentUser = _userService.GetUser(userDto, passwordHash);
            _userService.Add(currentUser);
            return StatusCode(StatusCodes.Status201Created, userDto);
        }
        else
        {
            Debug.Assert(validationResult.Errors != null, "validationResult.Errors != null");
            foreach (var error in validationResult.Errors)
            {
                _logger.LogError($"Errors occurred while validation model: {error.ErrorMessage}");
            }

            return StatusCode(StatusCodes.Status500InternalServerError, validationResult.Errors);
        }
    }

    [HttpPost("login")]
    public ActionResult<User> Login(LoginDTO? userDto)
    {
        if (userDto == null)
            return StatusCode(StatusCodes.Status400BadRequest, "Invalid object identification.");

        User? user = _userService.GetAll().FirstOrDefault(u => u.Email == userDto.Email);
        if (user == null)
            return StatusCode(StatusCodes.Status404NotFound, "There is not such user in DataBase.");

        var modelValidator = new ModelValidatorService();
        var validationResult = modelValidator.ValidateModel(user);

        if (validationResult.IsValid)
        {
            // if (currentUser.Email != userDto.Email)
            //     return BadRequest("Wrong email.");

            if (!BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password))
                return BadRequest("Wrong password.");
            
            var token = CreateToken(user);
            var refreshToken = _authService.GenerateRefreshToken();
            _authService.SetRefreshToken(user, refreshToken, Response);
            var responseToken = new { token , user};
            
            return StatusCode(StatusCodes.Status201Created, responseToken);
        }
        else
        {
            Debug.Assert(validationResult.Errors != null, "validationResult.Errors != null");
            foreach (var error in validationResult.Errors)
            {
                _logger.LogError($"Errors occurred while validation model: {error.ErrorMessage}");
            }

            return StatusCode(StatusCodes.Status500InternalServerError, validationResult.Errors);
        }
    }
    
    [HttpGet, Authorize]
    public ActionResult<string> GetAuthorizedUserName()
    {
        return StatusCode(StatusCodes.Status200OK, _userService.GetName());
    }

    [HttpPost("refresh-token"), Authorize]
    public async Task<ActionResult<string>> RefreshToken()
    {
        var currentUser = _userService.GetAll().FirstOrDefault(u => u.Name == _userService.GetName());
        var refreshToken = Request.Cookies["refreshToken"];

        if (currentUser == null || !currentUser.RefreshToken.Equals(refreshToken))
            return StatusCode(StatusCodes.Status401Unauthorized, "Invalid refresh token.");
        else if ( currentUser.TokenExpires < DateTime.Now)
            return StatusCode(StatusCodes.Status401Unauthorized, "Token expired.");

        string token = CreateToken(currentUser);
        var newRefreshToken = _authService.GenerateRefreshToken();
        _authService.SetRefreshToken(currentUser, newRefreshToken, Response);
        
        return StatusCode(StatusCodes.Status200OK, token);
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim("Name", user.Name),
            new Claim("Email", user.Email),
            new Claim("Role", user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}