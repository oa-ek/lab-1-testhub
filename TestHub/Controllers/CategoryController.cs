using Microsoft.AspNetCore.Mvc;
using TestHub.Infrastructure.Repository;
using TestHub.Core.Models;
using TestHub.Core.Dtos;
namespace TestHub.Controllers;

[Route("Category")]
[ApiController]
public class CategoryController: Controller
{
    
        private readonly GenericRepository<Category> _categoryRepository;
        private readonly ILogger _logger;
      
        
        public CategoryController(GenericRepository<Category> categoryRepository, ILogger<CategoryController> logger)
        {
            _categoryRepository =categoryRepository;
            _logger = logger;

            // Log the type being injected
            _logger.LogInformation($"Injected categoryRepository of type: {categoryRepository.GetType()}");
        }
   
        
        [HttpPost]
        public IActionResult CreateCategory(CategoryDto categoryDTO)
        {
                var category = new Category
                {
                    Title = categoryDTO.Title,
                };
                _categoryRepository.Insert(category);
                // return RedirectToAction("GetAllCategory");
                ViewBag.CreatedCategory = category;

                return Ok();
        }
        
        [HttpGet]
        public IActionResult GetAllCategory()
        {
            var categories = _categoryRepository.Get();
            return View("crud/GetAllCategory",categories);
        }
        
        [HttpPatch("UpdateCategory/{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoryDto update)
        {
            var category=_categoryRepository.GetByID(id);
            category.Title = update.Title;
            _categoryRepository.Update(category);
            return Ok();
        }
        
        
        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            _categoryRepository.Delete(id);
            return Ok();
        }
        
             
}