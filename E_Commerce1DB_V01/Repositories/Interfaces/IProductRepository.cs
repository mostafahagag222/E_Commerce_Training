using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Repositories;
using E_Commerce1DB_V01.Repositories.Interfaces;
using E_Commerce2Business_V01.Payloads;

namespace E_Commerce1DB_V01.Entities
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<PaginationDTO<GetProductsDTO>> GetProductsPageAsync(GetProductsPagePayload payload);
        Task<int> GetUnitsInStockForOneProductAsync(int id);
    }
}
