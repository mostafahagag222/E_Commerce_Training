using E_Commerce1DB_V01;
using E_Commerce1DB_V01.DTOs;
using E_Commerce2Business_V01.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce2Business_V01.Interfaces
{
    public interface IProductService
    {
        Task<List<GetBrandsDTO>> GetBrands();
        Task<List<ProductDTO>> GetProductsDetailsForCartItems(string cartId);
        Task<PaginationDTO<GetProductsDTO>> GetProductsPageAsync(GetProductsPayload payload);
        Task<List<GetTypesDTO>> GetTypes();
        Task<int> GetUnitsInStockForOneProductAsync(int id);
    }
}
