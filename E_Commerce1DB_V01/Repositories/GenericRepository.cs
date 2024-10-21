using E_Commerce1DB_V01.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
//this class works with unit of work as it have no save changes methods
namespace E_Commerce1DB_V01
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ECPContext _context;
        public GenericRepository(ECPContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void DeleteAsync(T entity)

        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int? id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public void UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
