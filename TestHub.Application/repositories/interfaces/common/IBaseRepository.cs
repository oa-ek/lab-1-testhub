using Domain.common;

namespace Application.repositories.interfaces.common;

public interface IBaseRepository<T> where T : BaseAuditableEntity
{
    public Task InsertAsync(T entity);
    public Task UpdateAsync(T entity);
    public Task<T> DeleteAsync(T entity);

    public Task<T?> GetByIdAsync(int id);
    public Task<IEnumerable<T>?> GetAllAsync();

}