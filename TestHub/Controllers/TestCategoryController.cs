using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestHub.Core.Models;
using TestHub.Infrastructure.Services;

namespace TestHub.Controllers;

[Route("api/TestCategory")]
[Produces("application/json")]
[ApiController]
[Authorize]
public class TestCategoryController : Controller
{
    private readonly TestService _testService;

    public TestCategoryController(TestService testService)
    {
        _testService = testService;
    }

    [HttpGet("{id:int}", Name = "GetTestCategories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ICollection<Category>> Get(int id)
    {
        return Ok(_testService.GetCategoriesByTestId(id));
    }
}