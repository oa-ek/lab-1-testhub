using Application.common.models;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TestHub.Web.controllers.common;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

    protected ActionResult HandleResult<T>(Result<T> result)
    {
        Guard.Against.Null(result, nameof(result));

        if (result.Succeeded && result.Value != null)
            return Ok(result.Value);

        if (result.Value is null)
            return NotFound();

        return BadRequest(result.Errors);
    }
}