using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestHub.Core.Dtos;
using TestHub.Infrastructure.Repository;
using TestHub.Core.Models;
using TestHub.Infrastructure.Services;

namespace TestHub.Controllers;

public class CategoryController: Controller
{
    
        private readonly GenericRepository<Category> _categoryRepository;
        private readonly ILogger _logger;
      
        
        public CategoryController(GenericRepository<Category> categoryRepository, ILogger<CategoryController> logger)
        {
            _categoryRepository =categoryRepository;
            _logger = logger;
        }
        //
        // [HttpGet]
        // public IActionResult  Index()
        // {
        //     var categories = _categoryRepository.Get();
        //     Console.WriteLine(categories);
        //
        //     // Return JSON data
        //     return Json(category);
        // }
        
    
        
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categories = _categoryRepository.Get();
            
            return View("Index", categories);
        }

        [HttpPost]
        public IActionResult Create(CategoryDto? categoryDto)
        {
            if (categoryDto == null )
                return StatusCode(StatusCodes.Status400BadRequest, categoryDto);
            
            var modelValidator = new ModelValidatorService();
            var validationResult = modelValidator.ValidateModel(categoryDto);
            
            if (validationResult.IsValid)
            {
                var category =new Category()
                {
                    Title =categoryDto.Title
                };
                _categoryRepository.Insert(category);
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
                _categoryRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while deleting answer with {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Edit(int id, CategoryDto categoryDto)
        {
            try
            {
                var category = _categoryRepository.GetByID(id); 
                category.Title = categoryDto.Title;
                _categoryRepository.Update(category);
                return RedirectToAction("Index");
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while deleting answer with {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }


    }
