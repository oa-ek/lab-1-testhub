namespace Application.contracts.persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> Get(int id);
        Task<IReadOnlyList<T>> GetAll();
        Task<bool> Exists(int id);
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task DeleteRange(IEnumerable<T> entities);
        Task CreateRange(IEnumerable<T> entities);
        Task UpdateRange(IEnumerable<T> entities);
    }
}