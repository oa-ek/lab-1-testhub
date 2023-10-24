using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestHub.Core.Dtos;
using TestHub.Core.Models;
using TestHub.Infrastructure.Services;
using InvalidOperationException = System.InvalidOperationException;

namespace TestHub.Controllers;

[Route("api/Question")]
[Produces("application/json")]
[ApiController]
[Authorize]
public class QuestionController : Controller
{
    private readonly QuestionService _questionService;
    private readonly AnswerService _answerService;
    private readonly FileService _fileService;

    public QuestionController(QuestionService questionService, AnswerService answerService, ILogger<QuestionController> logger, FileService fileService)
    {
        _questionService = questionService;
        _answerService = answerService;
        _fileService = fileService;

        // Log the type being injected
        logger.LogInformation($"Injected questionService of type: {questionService.GetType()}");
    }

    [HttpGet("getByTest/{testid:int}",  Name = "GetQuestionByTest")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ICollection<Question>> GetAllQuestionByTest(int testId)
    {
        return Ok(_questionService.GetAllByTest(testId));
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
    public async Task<ActionResult<Question>> CreateQuestionAnswer(int testId, [FromBody] QuestionDto[]? questionDtos)
    {
        if (questionDtos == null)
            return StatusCode(StatusCodes.Status400BadRequest, "Invalid object identification.");
    
        var modelValidator = new ModelValidatorService();
        var types = _questionService.GetQuestionTypes();
        foreach (var questionDto in questionDtos)
        {
            var validationResult = modelValidator.ValidateModel(questionDto);
            var type = types.FirstOrDefault(t => t.Type.Equals(questionDto.Type)) ??
                       throw new InvalidOperationException("There is not such question's type in Database.");
            if (validationResult.IsValid)
            {
                var question = new Question
                {
                    TestId = testId,
                    Title = questionDto.Title,
                    Description = questionDto.Description,
                    Image = questionDto.q_image == null ? null :  await _fileService.UploadImage(questionDto.q_image), 
                    Type = type
                };
                _questionService.Add(question);
                foreach (var answerDto in questionDto.Answers)
                {
                    var answer = new Answer
                    {
                        QuestionId = question.Id,
                        Text = answerDto.Text,
                        IsCorrect = answerDto.IsCorrect,
                        IsStrictText = answerDto.IsStrictText,
                        Image = answerDto.a_image == null ? null : await _fileService.UploadImage(answerDto.a_image)
                    };
                    _answerService.Add(answer);
                }
            }
        }
   
        return StatusCode(StatusCodes.Status201Created, questionDtos);
    }
    
    [HttpDelete("{id:int}", Name = "DeleteQuestion")]
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
    
    [HttpPut("{testId:int}", Name = "UpdateQuestionAndAnswer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateQuestionAndAnswer(int testId, [FromBody]  AnswerArray data)
    {
        /*if (data == null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, "Invalid object identification.");
        }

        Question? questionToUpdate = _questionService.GetAll()
            .FirstOrDefault(q => q.Id == data.Answers.FirstOrDefault().QuestionId);

        if (questionToUpdate == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "There is not such question in DataBase.");
        }
        var questionChanging = new QuestionDto
        {
            Title = data.Title,
            Type = ""
        };
        _questionService.Update(questionToUpdate, questionChanging);
        var answerd = data.Answers;
           
        for (int i = 0; i < answerd.Count(); i++)
        {
            Answer? answerToUpdate = _answerService.GetAll()
                .FirstOrDefault(q => q.Id == answerd[i].Id);
            var answerChanging = new Answer
            {
                QuestionId = questionToUpdate.Id,
                Text = answerd[i].Text,
                IsCorrect = true,
                IsStrictText = true
            };
            _answerService.Update(answerToUpdate, answerChanging);
        }
        */
        return StatusCode(StatusCodes.Status201Created);    
    }
}