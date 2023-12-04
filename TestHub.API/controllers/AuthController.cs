using Application.contracts.identity;
using Application.models.identity;
using TestHub.API.models.identity;

namespace TestHub.API.controllers;

[Route("api/Auth")]
[Produces("application/json")]
[ApiController]
public class AuthController : Controller
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<BaseCommandResponse<AuthResponse>> Login(AuthRequest? request)
    {
        var response = await _service.Login(request);
        return response;
    }
}