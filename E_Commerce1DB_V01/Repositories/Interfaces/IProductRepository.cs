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
        Task<List<GetBrandsDTO>> GetBrandsAsync();
        Task<List<ProductDTO>> GetProductsDetailsForCartItems(string cartId);
        Task<PaginationDTO<GetProductsDTO>> GetProductsPage(GetProductsPayload payload);
        Task<List<GetTypesDTO>> GetTypesAsync();
        Task<int> GetUnitsInStockForOneProductAsync(int id);
    }
}
