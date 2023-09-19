using Microsoft.AspNetCore.Mvc;
using TestHub.Infrastructure.Repository;
using TestHub.Core.Models;
namespace TestHub.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController: Controller
{
        private readonly GenericRepository<User> _categoryRepository;
        
        public CategoryController(GenericRepository<User> categoryRepository)
        {
            this._categoryRepository =categoryRepository;
        }
       
        [HttpGet]
        public string Index()
        {
            var categories = _categoryRepository.GetById(1);
            Console.WriteLine(categories);

            return $"{categories}";
        }
}