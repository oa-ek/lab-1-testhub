using Application.repositories.interfaces;
using Application.repositories.interfaces.common;
using Domain.common;
using TestHub.Infrastructure.data.repositories.common;

namespace TestHub.Infrastructure.data.repositories.factories;

public class RepositoryFactory : IRepositoryFactory
{
    private readonly TestHubDbContext _context;

    public RepositoryFactory(TestHubDbContext context)
    {
        _context = context;
    }

    public IBaseRepository<T> CreateRepository<T>() where T : BaseAuditableEntity
    {
        return new BaseRepository<T>(_context);
    }
}