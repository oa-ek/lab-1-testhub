namespace Application.repositories.interfaces.common;

public interface IDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    DbSet<T> Set<T>() where T : class;
}