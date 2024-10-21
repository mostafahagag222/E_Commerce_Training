using E_Commerce1DB_V01.DTOs;
using E_Commerce2Business_V01.Payloads;

namespace E_Commerce1DB_V01.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int? id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
    }
}
