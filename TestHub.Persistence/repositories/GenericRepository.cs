using Application.contracts.persistence;

namespace TestHub.Persistence.repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly TestHubDbContext _context;

    public GenericRepository(TestHubDbContext context)
    {
        _context = context;
    }

    public async Task<T?> Get(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<bool> Exists(int id)
    {
        var entity = await Get(id);
        return entity != null;
    }

    public async Task<T> Add(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
        await _context.SaveChangesAsync();
    }
    
    public async Task CreateRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
            await _context.AddAsync(entity);
        
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
            _context.Entry(entity).State = EntityState.Modified;
        
        await _context.SaveChangesAsync();
    }

}