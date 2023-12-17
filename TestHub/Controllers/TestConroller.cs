using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestHub.Core.Dtos;
using TestHub.Core.Models;
using TestHub.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
namespace TestHub.Controllers;

[Route("api/Test")]
[Produces("application/json")]
[ApiController]
[Authorize]
public class TestController : Controller
{
    private readonly TestService _testService;
    private readonly TestSessionService _testSessionService;
    private readonly UserService _userService;
    private readonly QuestionService _questionService;
    private readonly ILogger _logger;

    public TestController(ILogger<TestController> logger, TestService testService, UserService userService, TestSessionService testSessionService,
        QuestionService questionService)
    {
        _logger = logger;
        _testService = testService;
        _userService = userService;
        _testSessionService = testSessionService;
        _questionService = questionService;
        // Log the type being injected
        _logger.LogInformation($"Injected testService of type: {testService.GetType()}");
        _logger.LogInformation($"Injected userService of type: {userService.GetType()}");
    }

    [HttpGet("search/{text}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ICollection<Test>> Search(string text)
    {
        return Ok(_testService.Search(text));
    }
    
    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ICollection<Test>> GetAll()
    {
        return Ok(_testService.GetAll());
    }

    [HttpGet("GetByUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ICollection<Test>> GetByUser()
    {
        return Ok(_testService.GetAll().Where(u => u.OwnerId == _userService.GerRegistrationUser()!.Id));
    }
    
    [HttpGet("GetPublicTests")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ICollection<Test>> GetPublicTests()
    {
        return Ok(_testService.GetAll().Where(u => u.IsPublic));
    }

    [HttpGet("{id:int}", Name = "GetTest")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Test> GetTest(int id)
    {
        Test? searchTest = _testService.GetAll().FirstOrDefault(r => r.Id == id);
        if (searchTest == null)
            return StatusCode(StatusCodes.Status404NotFound, "There is no such test in the database.");

        return StatusCode(StatusCodes.Status200OK, searchTest);
    }
    
    [HttpGet("TestMetadata/{testId:int}", Name = "GetMetadata")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<IEnumerable<TestMetadata>> GetMetadata(int testId)
    {
        var metadata = _testService.GetMetadata().Where(r => r.TestId == testId);

        return StatusCode(StatusCodes.Status200OK, metadata);
    }
    
    [HttpPut("TestMetadata/{testId:int}", Name = "UpdateMetadata")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<TestMetadata> UpdateMetadata(int testId, [FromBody] bool isLike)
    {
        var like = Convert.ToByte(isLike);
        var user = _userService.GerRegistrationUser()!;
        var metadata = _testService.GetMetadata().FirstOrDefault(r => r.TestId == testId && r.User == user);
        if (metadata == null)
            _testService.AddMetadata(user.Id, testId, like);
        
        if (metadata != null && metadata.Like != like)
            _testService.UpdateMetadata(metadata, like);

        return StatusCode(StatusCodes.Status200OK, metadata);
    }
    
    [HttpDelete("TestMetadata/{testId:int}", Name = "DeleteMetadata")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteMetadata(int testId)
    {
        var user = _userService.GerRegistrationUser()!;
        var metadata = _testService.GetMetadata().FirstOrDefault(r => r.TestId == testId && r.User == user);
        
        if (metadata == null)
            return StatusCode(StatusCodes.Status404NotFound, "There is not such metadata in DataBase.");
        
        _testService.DeleteMetadata(metadata);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<TestDto> CreateTest([FromBody] TestDto? testDto)
    {
        int ownerId = _userService.GerRegistrationUser()!.Id;
        if (testDto == null)
            return StatusCode(StatusCodes.Status400BadRequest, testDto);

        var modelValidator = new ModelValidatorService();
        var validationResult = modelValidator.ValidateModel(testDto);

        if (validationResult.IsValid)
        {
            var createdTest = new Test
            {
                Title = testDto.Title,
                Description = testDto.Description,
                Duration = testDto.Duration,
                IsPublic = testDto.IsPublic,
                OwnerId = ownerId,
                Status = testDto.Status,
                CreatedAt = DateTime.Now
            };

            _testService.Add(createdTest);
            _testService.SetCategories(createdTest, testDto.Categories);

            return StatusCode(StatusCodes.Status201Created, createdTest);
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

    [HttpDelete("{id:int}", Name = "DeleteTest")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteTest(int id)
    {
        Test? testToDelete = _testService.GetAll().FirstOrDefault(c => c.Id == id);
        if (testToDelete == null)
            return StatusCode(StatusCodes.Status404NotFound, "There is not such test in DataBase.");

        _testService.DeleteCategories(testToDelete);
        _testService.DeleteQuestionsAndAnswers(testToDelete);

        // Видалити видаляємий тест
        _testService.Delete(testToDelete);

        return StatusCode(StatusCodes.Status204NoContent);
    }


    [HttpPut("{id:int}", Name = "UpdateTest")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateTest(int id, TestDto? testDto)
    {
        if (testDto == null)
            return StatusCode(StatusCodes.Status400BadRequest, "Invalid object identification.");

        Test? testToUpdate = _testService.GetAll().FirstOrDefault(c => c.Id == id);
        if (testToUpdate == null)
            return StatusCode(StatusCodes.Status404NotFound, "There is not such test in DataBase.");

        var modelValidator = new ModelValidatorService();
        var validationResult = modelValidator.ValidateModel(testDto);

        if (validationResult.IsValid)
        {
            _testService.Update(testToUpdate, testDto);
            _testService.UpdateCategories(testToUpdate, testDto.Categories);
            return StatusCode(StatusCodes.Status201Created, testDto);
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
    
    
    //createTest User session
    [HttpPost("createTestSession")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<TestSession> CreateSession([FromBody] TestSessionDto? testSessionDto)
    {
        if (testSessionDto == null)
            return StatusCode(StatusCodes.Status400BadRequest, testSessionDto);
    
        var modelValidator = new ModelValidatorService();
        var validationResult = modelValidator.ValidateModel(testSessionDto);

        var user = _userService.GetById(testSessionDto.UserId);
        var test = _testService.GetById(testSessionDto.TestId);
        if (validationResult.IsValid)
        {
            var createdTestSession = new TestSession()
            {
                UserId = testSessionDto.UserId,
                TestId = testSessionDto.TestId,
                IsTraining = testSessionDto.IsTraining,
                StartedAt = DateTime.Now
            };
    
            _testSessionService.AddSession(createdTestSession);
    
            return StatusCode(StatusCodes.Status201Created, createdTestSession);
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

    [HttpPatch("testSession")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<TestSession> UpdateSession([FromBody] UpdateTestSessionDto? testSessionDto)
    {
        if (testSessionDto == null)
            return StatusCode(StatusCodes.Status400BadRequest, testSessionDto);
    
        var modelValidator = new ModelValidatorService();
        var validationResult = modelValidator.ValidateModel(testSessionDto);
        TestSession? testSession = _testSessionService.GetAll().FirstOrDefault(c => c.Id == testSessionDto.Id);
        testSession.FinishedAt = DateTime.Now;
        testSession.Result = testSessionDto.Result;
        if (validationResult.IsValid)
        {
            
            _testSessionService.Update(testSession);
    
            return StatusCode(StatusCodes.Status201Created, testSession);
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


    [HttpPost("statusQuestion")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<StatusQuestionSessionDto> CreateStatusQuestion([FromBody] StatusQuestionSessionDto? statusQuestionSessionDto)
    {
        if (statusQuestionSessionDto == null)
            return StatusCode(StatusCodes.Status400BadRequest, statusQuestionSessionDto);
    
        var modelValidator = new ModelValidatorService();
        var validationResult = modelValidator.ValidateModel(statusQuestionSessionDto);
        
        var question = _questionService.GetById(statusQuestionSessionDto.QuestionId);
        var testSession = _testSessionService.GetById(statusQuestionSessionDto.SessionId);
        var statusSession= _testSessionService.GetAllStatuses().
            FirstOrDefault(c => c.QuestionId == statusQuestionSessionDto.QuestionId&&
                                c.SessionId == statusQuestionSessionDto.SessionId);
        
        if (validationResult.IsValid)
        {
            if (statusSession != null)
            {
                
                _testSessionService.UpdateStatus(statusSession, statusQuestionSessionDto);
                return StatusCode(StatusCodes.Status200OK, statusSession);
            }
            else
            {
                var createdStatusTestSession = new StatusSessionQuestion()
                {
                    SessionId = statusQuestionSessionDto.SessionId,
                    QuestionId = statusQuestionSessionDto.QuestionId,
                    IsCorrect = statusQuestionSessionDto.IsCorrect ?? null,
                    Point = statusQuestionSessionDto.Point ?? null,
                    Attepts = 1,
                    Question = question,
                    Session = testSession
                };
    
                _testSessionService.AddStatus(createdStatusTestSession);
                return StatusCode(StatusCodes.Status201Created, createdStatusTestSession);
            }
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
    
    
     [HttpGet("getTestSession/{testSessionId:int}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetResults(int testSessionId)
    {
        if (testSessionId == null)
            return StatusCode(StatusCodes.Status400BadRequest);
    
        // TestSession? testSession = _testSessionService.GetAll().FirstOrDefault(r => r.Id == testSessionId);
        TestSession? testSession = _testSessionService.GetAll()
            .FirstOrDefault(ts => ts.Id == testSessionId);
        
        
        return StatusCode(StatusCodes.Status200OK, testSession);
    
    }
    
    [HttpGet("getResults/{testSessionId:int}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetSessionResults(int testSessionId)
    {
        if (testSessionId == null)
            return StatusCode(StatusCodes.Status400BadRequest);
    
        // TestSession? testSession = _testSessionService.GetAll().FirstOrDefault(r => r.Id == testSessionId);
        var statusSessionQuestion = _testSessionService.GetAllStatusesSession(testSessionId);
        
        return StatusCode(StatusCodes.Status200OK, statusSessionQuestion);
    
    }

}