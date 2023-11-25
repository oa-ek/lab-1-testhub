using Application.repositories.interfaces;
using Domain.entities;
using TestHub.Infrastructure.data.repositories.common;

namespace TestHub.Infrastructure.data.repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(TestHubDbContext context) : base(context)
    {
    }
}