using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestHub.Core.Dtos;
using TestHub.Infrastructure.Repository;
using TestHub.Core.Models;
using TestHub.Infrastructure.Services;

namespace TestHub.Controllers;

[Route("api/Category")]
[Produces("application/json")]
[ApiController]
public class CategoryController : Controller
{
    private readonly CategoryService _categoryService;
    private readonly ILogger _logger;
    
    public CategoryController(ILogger<CategoryController> logger, CategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;

        // Log the type being injected
        _logger.LogInformation($"Injected categoryService of type: {categoryService.GetType()}");
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ICollection<CategoryDto>> Get()
    {
        return Ok(_categoryService.GetAll());
    }
    
    [HttpGet("{id:int}", Name = "GetCategory")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Category> GetCategory(int id)
    {
        Category? searchCategory = _categoryService.GetAll().FirstOrDefault(r=>r.Id==id);
        if (searchCategory == null)
            return StatusCode(StatusCodes.Status404NotFound, "There is no such category in the database.");

        return StatusCode(StatusCodes.Status200OK, searchCategory);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<CategoryDto> CreateCategory([FromBody] CategoryDto? categoryDto)
    {
        if (categoryDto == null)
            return StatusCode(StatusCodes.Status400BadRequest, categoryDto);

        var modelValidator = new ModelValidatorService();
        var validationResult = modelValidator.ValidateModel(categoryDto);

        if (validationResult.IsValid)
        {
            _categoryService.Add(new Category{Title = categoryDto.Title});
            return StatusCode(StatusCodes.Status201Created, categoryDto);
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
    
    [HttpDelete("{id:int}", Name = "DeleteCategory")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        Category? categoryToDelete = _categoryService.GetAll().FirstOrDefault(c => c.Id == id);
        if (categoryToDelete == null)
            return StatusCode(StatusCodes.Status404NotFound, "There is not such category in DataBase.");
    
        _categoryService.Delete(categoryToDelete);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpPut("{id:int}", Name = "UpdateCategory")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateCategory(int id, CategoryDto? categoryDto)
    {
        if (categoryDto == null)
            return StatusCode(StatusCodes.Status400BadRequest, "Invalid object identification.");
        
        Category? categoryToUpdate = _categoryService.GetAll().FirstOrDefault(c => c.Id == id);
        if (categoryToUpdate == null)
            return StatusCode(StatusCodes.Status404NotFound, "There is not such category in DataBase.");
        
        var modelValidator = new ModelValidatorService();
        var validationResult = modelValidator.ValidateModel(categoryDto);

        if (validationResult.IsValid)
        {
            _categoryService.Update(categoryToUpdate, categoryDto);
            return StatusCode(StatusCodes.Status201Created, categoryDto);
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