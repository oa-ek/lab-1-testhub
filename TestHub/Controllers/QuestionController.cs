using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestHub.Core.Dtos;
using TestHub.Core.Models;
using TestHub.Infrastructure.Services;

namespace TestHub.Controllers;

[Route("api/Question")]
[Produces("application/json")]
[ApiController]
public class QuestionController : Controller
{
    private readonly QuestionService _questionService;
    private readonly FileService _fileService;
    private readonly ILogger<QuestionController> _logger;

    public QuestionController(QuestionService questionService, FileService fileService, ILogger<QuestionController> logger)
    {
        _questionService = questionService;
        _fileService = fileService;
        _logger = logger;

        // Log the type being injected
        _logger.LogInformation($"Injected questionService of type: {questionService.GetType()}");
        _logger.LogInformation($"Injected fileService of type: {fileService.GetType()}");
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ICollection<Question>> Get()
    {
        return Ok(_questionService.GetAll());
    }
    
    [HttpGet("{id:int}", Name = "GetQuestion")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Category> GetQuestion(int id)
    {
        Question? searchQuestion = _questionService.GetAll().FirstOrDefault(r=>r.Id==id);
        if (searchQuestion == null)
            return StatusCode(StatusCodes.Status404NotFound, "There is no such question in the database.");

        return StatusCode(StatusCodes.Status200OK, searchQuestion);
    }

    [HttpPost("{testId:int}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<CategoryDto> CreateCategory(int testId, [FromBody] object? jsonObject)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{id:int}", Name = "Delete")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        Question? questionToDelete = _questionService.GetAll().FirstOrDefault(q => q.Id == id);
        if (questionToDelete == null)
            return StatusCode(StatusCodes.Status404NotFound, "There is not such question in DataBase.");

        _questionService.Delete(questionToDelete);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpPut("{id:int}", Name = "UpdateQuestion")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateQuestion(int id, QuestionDto? questionDto)
    {
        if (questionDto == null)
            return StatusCode(StatusCodes.Status400BadRequest, "Invalid object identification.");
        
        Question? questionToUpdate = _questionService.GetAll().FirstOrDefault(q => q.Id == id);
        if (questionToUpdate == null)
            return StatusCode(StatusCodes.Status404NotFound, "There is not such question in DataBase.");
        
        var modelValidator = new ModelValidatorService();
        var validationResult = modelValidator.ValidateModel(questionDto);

        if (validationResult.IsValid)
        {
            _questionService.Update(questionToUpdate, questionDto);
            return StatusCode(StatusCodes.Status201Created, questionDto);
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