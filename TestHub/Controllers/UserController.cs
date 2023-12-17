using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestHub.Core.Dtos;
using TestHub.Infrastructure.Services;

namespace TestHub.Controllers;

[Route("api/User")]
[Produces("application/json")]
[ApiController]
[Authorize]
public class UserController : Controller
{
    private readonly UserService _userService;
    private readonly ILogger _logger;

    public UserController(UserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }
    
    [HttpPut("{token}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateUser(string token, UserDto? userDto)
    {
        if (userDto == null)
            return StatusCode(StatusCodes.Status400BadRequest, "Invalid object identification.");

        token = token.Trim('"');
        var email = new JwtSecurityToken(jwtEncodedString: token).Claims.First(c=> c.Type == "Email").Value;
        
        var userToUpdate = _userService.GetAll().FirstOrDefault(c => c.Email == email);
        if (userToUpdate == null)
            return StatusCode(StatusCodes.Status404NotFound, "There is not such test in DataBase.");

        var modelValidator = new ModelValidatorService();
        var validationResult = modelValidator.ValidateModel(userDto);

        if (validationResult.IsValid)
        {
            _userService.Update(userToUpdate, userDto);
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
}