using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestHub.Core.Dtos;
using TestHub.Core.Models;
using TestHub.Infrastructure.Repository;
using TestHub.Infrastructure.Services;

namespace TestHub.Controllers
{
    [Route("api/Answer")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class AnswerController : Controller
    {
        private readonly ILogger<AnswerController> _logger;
        private readonly FileService _fileService;
        private readonly AnswerService _answerService;

        public AnswerController(ILogger<AnswerController> logger,
            FileService fileService,
            AnswerService answerService,
            GenericRepository<Answer> answerRepository)
        {
            _logger = logger;
            _fileService = fileService;
            _answerService = answerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ICollection<CategoryDto>> Get()
        {
            return Ok(_answerService.GetAll());
        }

        [HttpGet("{id:int}", Name = "GetAnswer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Category> GetAnswer(int id)
        {
            Answer? searchAnswer = _answerService.GetAll().FirstOrDefault(r => r.Id == id);
            if (searchAnswer == null)
                return StatusCode(StatusCodes.Status404NotFound, "There is no such answer in the database.");

            return StatusCode(StatusCodes.Status200OK, searchAnswer);
        }

        [HttpDelete("{id:int}", Name = "DeleteAnswer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            Answer? answerToDelete = _answerService.GetAll().FirstOrDefault(c => c.Id == id);
            if (answerToDelete == null)
                return StatusCode(StatusCodes.Status404NotFound, "There is not such answer in DataBase.");
        
            _answerService.Delete(answerToDelete);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /*[HttpPut("{id:int}", Name = "UpdateAnswer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateAnswer(int id, AnswerDto? answerDto)
        {
            if (answerDto == null)
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid object identification.");

            Answer? answerToUpdate = _answerService.GetAll().FirstOrDefault(c => c.Id == id);
            if (answerToUpdate == null)
                return StatusCode(StatusCodes.Status404NotFound, "There is not such answer in DataBase.");

            var modelValidator = new ModelValidatorService();
            var validationResult = modelValidator.ValidateModel(answerDto);

            if (validationResult.IsValid)
            {
                _answerService.Update(answerToUpdate, answerDto);
                return StatusCode(StatusCodes.Status201Created, answerDto);
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
        }*/
    }
}