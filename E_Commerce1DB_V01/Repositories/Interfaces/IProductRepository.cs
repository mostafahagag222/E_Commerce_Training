using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Entities;
using E_Commerce1DB_V01.Payloads;

namespace E_Commerce1DB_V01.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<PaginationDTO<GetProductsDTO>> GetProductsPageAsync(GetProductsPagePayload payload);
        Task<int> GetUnitsInStockForOneProductAsync(int id);
    }
}
