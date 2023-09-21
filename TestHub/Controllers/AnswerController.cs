using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestHub.Core.Dtos;
using TestHub.Core.Models;
using TestHub.Infrastructure.Repository;
using TestHub.Infrastructure.Services;

namespace TestHub.Controllers
{
    public class AnswerController : Controller
    {
        private readonly ILogger<AnswerController> _logger;
        private readonly FileService _fileService;
        private readonly GenericRepository<Answer> _answerRepository;
        private readonly AnswerService _answerService;

        public AnswerController(ILogger<AnswerController> logger,
            FileService fileService,
            AnswerService answerService,
            GenericRepository<Answer> answerRepository)
        {
            _logger = logger;
            _fileService = fileService;
            _answerService = answerService;
            _answerRepository = answerRepository;
        }
        
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var answers = _answerRepository.Get(null, null, "Question");
            
            return View("Index", answers);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create(AnswerDto? answerDto, IFormFile? file)
        {
            if (answerDto == null || file == null)
                return StatusCode(StatusCodes.Status400BadRequest, answerDto);
            
            string imagePath = _fileService.UploadImage(file).Result;
            if (!string.IsNullOrEmpty(imagePath))
            {
                answerDto.Image = imagePath;
            }
            
            var modelValidator = new ModelValidatorService();
            var validationResult = modelValidator.ValidateModel(answerDto);
            
            if (validationResult.IsValid)
            {
                var answer = _answerService.GetAswer(answerDto);
                _answerRepository.Insert(answer);
            }
            else
            {
                Debug.Assert(validationResult.Errors != null, "validationResult.Errors != null");
                foreach (var error in validationResult.Errors)
                {
                    _logger.LogError($"Exception occurred while validation model: {error.ErrorMessage}");
                }

                return StatusCode(StatusCodes.Status500InternalServerError, validationResult.Errors);
            }
            
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View("Edit");
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            try
            {
                _answerRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while deleting answer with {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}