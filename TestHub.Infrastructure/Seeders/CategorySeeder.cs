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
                Title = "Category 1"
            },
            new Category
            {
                Title = "Category 2"
            },
            new Category
            {
                Title = "Category 3"
            },
            new Category
            {
                Title = "Category 4"
            },
            new Category
            {
                Title = "Category 5"
            }
        };
        
        _context.AddRange(categories);
        _context.SaveChanges();
    }
}