using Application.contracts.persistence.authentication;
using Application.dtos.respondsDto;

namespace TestHub.API.controllers;

[Route("api/Auth")]
[Produces("application/json")]
[ApiController]
public class AuthController : Controller
{
    private readonly IAuthenticationService _authentication;

    public AuthController(IAuthenticationService authentication)
    {
        _authentication = authentication;
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<string?> Login(RequestLoginDto? request)
    {
        var authResult = await _authentication.Login(request);
        return authResult.ResponseObject != null ? authResult.ResponseObject!.Token : null;
    }
    
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<RespondAuthenticationDto> Register(RequestRegisterDto? request)
    {
        var authResult = await _authentication.Register(request);
        return authResult.ResponseObject!;
    }
}