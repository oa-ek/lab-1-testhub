using Application.contracts.persistence.authentication;
using Application.dtos.respondsDto;
using Application.features.user.requests.commands;
using TestHub.API.responces;

namespace TestHub.API.controllers;

[Route("api/Auth")]
[Produces("application/json")]
[ApiController]
public class AuthController : Controller
{
    private readonly IAuthenticationService _authentication;
    private readonly IMediator _mediator;

    public AuthController(IAuthenticationService authentication, IMediator mediator)
    {
        _authentication = authentication;
        _mediator = mediator;
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RespondAuthenticationDto>> Login(RequestLoginDto? request)
    {
        var authResult = await _authentication.Login(request);
        return BaseCommandResponse<RespondAuthenticationDto>.GetBaseCommandResponseMessage(authResult);
    }
    
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RespondAuthenticationDto>> Register(RequestRegisterDto? request)
    {
        var authResult = await _authentication.Register(request);
        return BaseCommandResponse<RespondAuthenticationDto>.GetBaseCommandResponseMessage(authResult);
    }
    
    [HttpPatch("verify")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RespondAuthenticationDto>> VerifiedEmail(VerifiedEmailDto? request)
    {
        var command = new SetVerifiedEmailRequest { VerifiedEmailDto = request };
        var response = await _mediator.Send(command);
        return BaseCommandResponse<RespondAuthenticationDto>.GetBaseCommandResponseMessage(response);
    }
}