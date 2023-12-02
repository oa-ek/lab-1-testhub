using Application.contracts.persistence;
using Application.dtos.sharedDTOs;

namespace TestHub.Persistence.repositories;

public class TestRepository : GenericRepository<Test>, ITestRepository
{
    private readonly TestHubDbContext _context;
    
    public TestRepository(TestHubDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Test?> GetTestWithDetails(int id)
    {
        var test = await _context.Tests!
            .Include(q => q.Questions)
            .ThenInclude(a=> a.Answers)
            .FirstOrDefaultAsync(q => q.Id == id);
        return test;
    }

    public async Task<List<Test>> GetTestWithDetails()
    {
        var tests = await _context.Tests!
            .Include(q => q.Questions)
            .ThenInclude(a=> a.Answers)
            .ToListAsync();
        return tests;
    }

    public async Task<List<Test>> GetTestWithDetailsByUser(int ownerId)
    {
        var tests = await _context.Tests!
            .Where(q => q.OwnerId == ownerId)
            .Include(q => q.Questions)
            .ThenInclude(a => a.Answers)
            .ToListAsync();

        return tests;
    }

    public async Task<List<Test>> GetTestWithDetailsByPublicity()
    {
        var tests = await _context.Tests!
            .Where(q => q.IsPublic == true)
            .Include(q => q.Questions)
            .ThenInclude(a => a.Answers)
            .ToListAsync();

        return tests;
    }
    
    public async Task SetCategories(Test test, CategoryDto categoryDto)
    {
        var category = await _context.Categories!
            .FirstOrDefaultAsync(c=> c.Title == categoryDto.Title);

        var testCategory = new TestCategory { Category = category!, Test = test};
        
        await _context.TestCategories!.AddAsync(testCategory);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteCategories(Test test)
    {
        var categories = _context.TestCategories!
            .Where(c=> c.Test == test);

        if (categories.Any())
        {
            _context.TestCategories!.RemoveRange(categories);
            await _context.SaveChangesAsync();
        }
    }
}