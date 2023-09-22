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
    private readonly AnswerService _answerService;
    private readonly FileService _fileService;
    private readonly ILogger<QuestionController> _logger;

    public QuestionController(QuestionService questionService, AnswerService answerService, ILogger<QuestionController> logger)
    {
        _questionService = questionService;
        _answerService = answerService;
        _logger = logger;

        // Log the type being injected
        _logger.LogInformation($"Injected questionService of type: {questionService.GetType()}");
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
    public ActionResult<Question> CreateQuestionAnswer(int testId, [FromBody]  AnswerArrayDto data)
    {
        if (data == null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, "Invalid object identification.");
        }
        var question = new Question
        {
            TestId = testId,
            Title = data.Title,
            TypeId = 1
        };
        _questionService.Add(question);
        var answerd = data.Answers;
           
        for (int i = 0; i < answerd.Count(); i++)
        {
            var answ = new Answer
            {
                QuestionId = question.Id,
                Text = answerd[i].Text,
                IsCorrect = true,
                IsStrictText = true
            };
            _answerService.Add(answ);
        }
        
        return StatusCode(StatusCodes.Status201Created, question);
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
    
    [HttpPut("{testId:int}", Name = "UpdateQuestionAndAnswer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateQuestionAndAnswer(int testId, [FromBody]  AnswerArray data)
    {
        if (data == null)
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
            TypeId = 1
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
        
        return StatusCode(StatusCodes.Status201Created, questionToUpdate);    
    }
}