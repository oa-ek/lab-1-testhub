using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TestHub.Core.Dtos;
using TestHub.Core.Dtos.AuthDTO;
using TestHub.Core.Models;
using TestHub.Infrastructure.Services;
using TestHub.Infrastructure.Services.Password;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
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
   public async Task<IActionResult> Register(UserDto? userDto)
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
            string token = CreateRandomToken();
            currentUser.Token=token;
            _userService.Add(currentUser);
            
            string fromEmail = _configuration["Smtp:FromEmail"];
            string username = _configuration["Smtp:Username"];
            string password = _configuration["Smtp:Password"];
            string emailUser = currentUser.Email;
            //
            var mimeServer = new MimeMessage();
            // email.From.Add(MailboxAddress.Parse(fromEmail));
            mimeServer.From.Add(new MailboxAddress("TestHub", fromEmail));
            mimeServer.To.Add(MailboxAddress.Parse(emailUser));
                 
            mimeServer.Subject = "Confirm email TestHub ";
            mimeServer.Body = new TextPart(TextFormat.Html) { Text = $"Для підтвердження пошти перейдіть за посиланням: <br />" +
            $"http://localhost:3000/confirm/{currentUser.Email}/{token}" +
            $"<br />Дякуємо за використання нашої платформи!" };
            
            
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(username, password);
            await smtp.SendAsync(mimeServer);
            await smtp.DisconnectAsync(true);
            
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
        
        if (user.IsVerifiedEmail ==false )
            return StatusCode(StatusCodes.Status404NotFound, "The email is not verified.");

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
    
    [HttpGet ("user")] [Authorize]
    public ActionResult<string> GetUser(string email)
    {
        User? user = _userService.GetAll().FirstOrDefault(u => u.Email == email);
        return StatusCode(StatusCodes.Status200OK, user);
    }
    

    [HttpPatch("verify")]
    public async Task<ActionResult<string>> VerifiedEmail(VerifiedEmaildDto? payload)
    {
        User? user = _userService.GetAll().FirstOrDefault(u => u.Email == payload.Email&& u.Token==payload.Token);
        user.IsVerifiedEmail = true;
        _userService.Update(user);
        return StatusCode(StatusCodes.Status200OK, user);
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim("Name", user.Name),
            new Claim("Email", user.Email),
            new Claim("Role", user.Role),
            new Claim("userId", user.Id.ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(24),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            User? user = _userService.GetAll().FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            
            string passwordResetToken = CreateRandomToken();
            DateTime resetTokenExpires = DateTime.Now.AddDays(1);
            _userService.AddResetPasswodToken(user, passwordResetToken, resetTokenExpires);
            
            
               string fromEmail = _configuration["Smtp:FromEmail"];
               string username = _configuration["Smtp:Username"];
               string password = _configuration["Smtp:Password"];
               string emailUser = user.Email;
        
                var mimeServer = new MimeMessage();
                // email.From.Add(MailboxAddress.Parse(fromEmail));
                mimeServer.From.Add(new MailboxAddress("TestHub", fromEmail));
                mimeServer.To.Add(MailboxAddress.Parse(emailUser));
                
                mimeServer.Subject = "Reset TestHub password";
                mimeServer.Body = new TextPart(TextFormat.Html) { Text = $"Для скидання пароля на сайті TestHub перейдіть за наступним посиланням: <br />" +
                                                                         $"http://localhost:3000/reset/{user.Email}/{passwordResetToken}" +
                                                                         $"<br />Дякуємо за використання нашої платформи!" };
        

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(username, password);
                await smtp.SendAsync(mimeServer);
                await smtp.DisconnectAsync(true);

            return StatusCode(StatusCodes.Status200OK, "Send url on email");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto request)
        {
            User? user = _userService.GetAll().FirstOrDefault(u => u.Email == request.Email&&u.PasswordResetToken == request.Token);
            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                return BadRequest("Invalid Token.");
            }
        
            
            var validationPwdResult = _passwordService.ValidatePassword(request.Password);
            if (!validationPwdResult.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validationPwdResult.ErrorMessage);


            if (request.Password != request.ConfirmPassword)
            {
                return BadRequest("Passwords not equal.");
            }
            
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            _userService.AddResetPasswod(user, passwordHash);
        
        
            return Ok("Password successfully reset.");
        }
      

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
        
        [HttpDelete("{id:int}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTest(int id)
        {
            User? user = _userService.GetAll().FirstOrDefault(c => c.Id == id);
            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound, "There is not such test in DataBase.");

            _userService.Delete(user);

            // Видалити видаляємий тест
            _userService.Delete(user);

            return StatusCode(StatusCodes.Status204NoContent);
        }
        
        [HttpGet("GetUsers")][Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ICollection<User>> GetUsers()
        {
            return Ok(_userService.GetAll());
        }
        
        
        // [HttpDelete("{id:int}", Name="DeleteUser")][Authorize]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // public IActionResult DeleteUser(int id)
        // {
        //     User? userToDelete = _userService.GetAll().FirstOrDefault(c => c.Id == id);
        //     if (userToDelete == null)
        //         return StatusCode(StatusCodes.Status404NotFound, "There is not such user in DataBase.");
        //
        //     _userService.Delete(userToDelete);
        //     return StatusCode(StatusCodes.Status204NoContent);
        // }
        
        
        
}