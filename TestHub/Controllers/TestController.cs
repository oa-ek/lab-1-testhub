using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestHub.Core.Context;
using TestHub.Core.Models;

namespace TestHub.Controllers;

[Route("api/TestApi")]
[Produces("application/json")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly TestHubDbContext _context;

    public TestController(TestHubDbContext context)
    {
        _context = context;
    }

    private List<Test> GetTestsList()
    {
        return _context.Tests
            .Include(t => t.Questions)
            .ThenInclude(q=>q.Answers)
            .Include(t => t.TestCategories)
            .Include(t => t.TestMetadata)
            .Include(t => t.TestSessions)
            .Include(t=>t.Owner)
            .ToList();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ICollection<Test>> GetTests()
    {
        return Ok(GetTestsList());
    }

    [HttpGet("{id:int}", Name = "GetTest")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Test> GetTest(int id)
    {
        if (id < 0)
            return StatusCode(StatusCodes.Status400BadRequest, "Invalid object identification.");

        var tests = GetTestsList();

        var searchTest = tests.FirstOrDefault(t => t.Id == id);
        
        if (searchTest == null)
            return StatusCode(StatusCodes.Status404NotFound, "There is no such test in the database.");

        return StatusCode(StatusCodes.Status200OK, searchTest);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<Test> CreateRecord([FromBody] Test? test)
    {
        if (test == null)
            return StatusCode(StatusCodes.Status400BadRequest, test);
        
        if (test.Id < 0)
            return StatusCode(StatusCodes.Status500InternalServerError, "Invalid object identification.");
        
        var validation = TestValidator.ValidateTest(test);

        if (!validation.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, validation.Errors);

        var tests = GetTestsList();
        
        if (tests.FirstOrDefault(p=>p.Id==test.Id) != null)
        {
            ModelState.AddModelError("IdValidationUniqueError", "Test with such identification already exists.");
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }


        _context.Tests.Add(test);
        _context.SaveChanges();
        
        return CreatedAtRoute("GetRecord", new { id = test.Id }, test);
    }
    
    [HttpDelete("{id:int}", Name = "DeleteTest")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteRecord(int id)
    {
        if (id < 0)
            return StatusCode(StatusCodes.Status400BadRequest, "Invalid object identification.");

        var tests = GetTestsList();
        var testToDelete = tests.FirstOrDefault(r => r.Id == id);
        if (testToDelete == null)
            return StatusCode(StatusCodes.Status404NotFound, "There is not such record in DataBase.");

        tests.Remove(testToDelete);
        _context.SaveChanges();
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    public IActionResult UpdateRecord(int id, Test? test)
    {
        if (test == null || id != test.Id)
            return StatusCode(StatusCodes.Status400BadRequest, "Invalid object identification.");

        var tests = GetTestsList();
        var testToUpdate = tests.FirstOrDefault(v => v.Id == id);
        if (testToUpdate == null)
            return StatusCode(StatusCodes.Status404NotFound, "There is not such test in DataBase.");
        
        var validation = TestValidator.ValidateTest(test);

        if (!validation.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, validation.Errors);
        
        testToUpdate.Title = test.Title;
        testToUpdate.Description = test.Description;
        testToUpdate.Duration = test.Duration;
        testToUpdate.OwnerId = test.OwnerId;
        testToUpdate.Status = test.Status;
        testToUpdate.IsPublic = test.IsPublic;
        testToUpdate.CreatedAt = test.CreatedAt;
        testToUpdate.UpdatedAt = test.UpdatedAt;
        testToUpdate.Owner = test.Owner;

        _context.Update(testToUpdate);
        _context.SaveChanges();
            
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpPatch("{id:int}", Name = "UpdatePartialTest")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    
    public IActionResult UpdatePartialRecord(int id, JsonPatchDocument<Test>? patchTest)
{
    if (patchTest == null || id < 0)
        return StatusCode(StatusCodes.Status400BadRequest, "Invalid object identification.");
    
    var testToUpdate = GetTestsList().FirstOrDefault(r => r.Id == id);
    if (testToUpdate == null)
        return StatusCode(StatusCodes.Status404NotFound, "There is not such test in DataBase.");
    
    var originalTest = new Test()
    {
        Title = testToUpdate.Title,
        Duration = testToUpdate.Duration,
        OwnerId = testToUpdate.OwnerId,
        Status = testToUpdate.Status,
        IsPublic = testToUpdate.IsPublic,
        CreatedAt = testToUpdate.CreatedAt,
        UpdatedAt = testToUpdate.UpdatedAt,
        Owner = testToUpdate.Owner
    };
    
    patchTest.ApplyTo(originalTest, ModelState);
    
    var validation = TestValidator.ValidateTest(originalTest);

    if (!validation.IsValid)
        return StatusCode(StatusCodes.Status400BadRequest, validation.Errors);

    patchTest.ApplyTo(testToUpdate, ModelState);
    return StatusCode(StatusCodes.Status204NoContent);
    }
}