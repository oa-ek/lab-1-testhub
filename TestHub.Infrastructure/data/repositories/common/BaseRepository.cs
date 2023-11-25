using Application.repositories.interfaces.common;
using Domain.common;
using Microsoft.EntityFrameworkCore;

namespace TestHub.Infrastructure.data.repositories.common
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseAuditableEntity
    {
        private readonly TestHubDbContext _context;

        public BaseRepository(TestHubDbContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<T> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>?> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
    }
}