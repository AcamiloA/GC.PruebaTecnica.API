using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Application.Repositories;
using PruebaTecnica.Infrastructure.Context;
using System.Linq.Expressions;

namespace PruebaTecnica.Infrastructure.Repositories
{
    public class Repository(ApplicationDBContext context) : IRepository
    {
        private readonly ApplicationDBContext _context = context;

        /// <inheritdoc/>
        public async Task AddAsync<T>(T entity) where T : class
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }


        /// <inheritdoc/>
        public async Task DeleteAsync<T>(int id) where T : class
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        /// <inheritdoc/>
        public async Task<List<T>> GetAllAsync<T>() where T : class
        {
            return await _context.Set<T>().ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<T?> GetByIdAsync<T>(int id) where T : class
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<List<T>> GetByWhereAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T?> GetOneByWhereAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await _context.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateAsync<T>(int id, T entity) where T : class
        {
            var existingEntity = await _context.Set<T>().FindAsync(id);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
