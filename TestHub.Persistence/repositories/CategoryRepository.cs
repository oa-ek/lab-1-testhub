using Application.contracts.persistence;

namespace TestHub.Persistence.repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    private readonly TestHubDbContext _context;
    
    public CategoryRepository(TestHubDbContext context) : base(context)
    {
        _context = context;
    }
}