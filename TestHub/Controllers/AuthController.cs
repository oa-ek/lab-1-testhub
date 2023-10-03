using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
    public static User CurrentUser = new User();
    private readonly IConfiguration _configuration;
    private readonly UserService _userService;
    private readonly PasswordService _passwordService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IConfiguration configuration, UserService userService, 
        ILogger<AuthController> logger, PasswordService passwordService)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _passwordService = passwordService ?? throw new ArgumentNullException(nameof(passwordService));
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
            CurrentUser = _userService.GetUser(userDto, passwordHash);
            _userService.Add(CurrentUser);
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
    public ActionResult<User> Login(UserDto? userDto)
    {
        if (userDto == null)
            return StatusCode(StatusCodes.Status400BadRequest, "Invalid object identification.");

        User? currentUser = _userService.GetAll().FirstOrDefault(u => u.Email == userDto.Email);
        if (currentUser == null)
            return StatusCode(StatusCodes.Status404NotFound, "There is not such user in DataBase.");

        var modelValidator = new ModelValidatorService();
        var validationResult = modelValidator.ValidateModel(currentUser);

        if (validationResult.IsValid)
        {
            if (currentUser.Name != userDto.Name)
                return BadRequest("Wrong username.");

            if (!BCrypt.Net.BCrypt.Verify(userDto.Password, currentUser.Password))
                return BadRequest("Wrong password.");
            
            string token = CreateToken(currentUser);
            
            return StatusCode(StatusCodes.Status201Created, token);
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

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}