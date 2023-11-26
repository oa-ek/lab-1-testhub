using Application.contracts.persistence;

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
}