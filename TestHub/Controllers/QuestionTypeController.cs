using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestHub.Core.Models;
using TestHub.Infrastructure.Services;

namespace TestHub.Controllers;

[Route("api/QuestionType")]
[Produces("application/json")]
[ApiController]
[Authorize]
public class QuestionTypeController : Controller
{
    private readonly QuestionService _questionService;
    private readonly ILogger<QuestionTypeController> _logger;

    public QuestionTypeController(QuestionService questionService, ILogger<QuestionTypeController> logger)
    {
        _questionService = questionService;
        _logger = logger;
        
        // Log the type being injected
        _logger.LogInformation($"Injected questionService of type: {questionService.GetType()}");
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ICollection<QuestionType>> Get()
    {
        return Ok(_questionService.GetQuestionTypes());
    }
}