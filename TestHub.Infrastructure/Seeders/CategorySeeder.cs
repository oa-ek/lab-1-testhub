using TestHub.Infrastructure.Context;
using TestHub.Core.Models;

namespace TestHub.Infrastructure.Seeders;

public class CategorySeeder
{
    private readonly TestHubDbContext _context;

    public CategorySeeder(TestHubDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (_context.Set<Category>().Any()) return;
        var categories = new List<Category>
        {
            new Category
            {
                Id = 1,
                Title = "Category 1"
            },
            new Category
            {
                Id = 2,
                Title = "Category 2"
            },
            new Category
            {
                Id = 3,
                Title = "Category 3"
            },
            new Category
            {
                Id = 4,
                Title = "Category 4"
            },
            new Category
            {
                Id = 5,
                Title = "Category 5"
            }
        };
        
        _context.AddRange(categories);
        _context.SaveChanges();
    }
}