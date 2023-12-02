using Application.contracts.persistence;

namespace TestHub.Persistence.repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    private readonly TestHubDbContext _context;

    public CategoryRepository(TestHubDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetByTestId(int testId)
    {
        var categories = await _context.TestCategories!
            .Where(tc => tc.TestId == testId)
            .Select(tc => tc.Category)
            .ToListAsync();

        return categories;
    }
}