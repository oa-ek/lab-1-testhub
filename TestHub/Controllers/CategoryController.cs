using Microsoft.AspNetCore.Mvc;
using TestHub.Infrastructure.Repository;
using TestHub.Core.Models;
namespace TestHub.Controllers;

[Route("api/category")]
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
       
        [HttpGet]
        public IActionResult  Index()
        {
            var category = _categoryRepository.Get();
            Console.WriteLine(category);

            // Return JSON data
            return Json(category);
        }
}