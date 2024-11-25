using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Payloads;

namespace E_Commerce2Business_V01.Interfaces
{
    public interface IProductService
    {
        Task<PaginationDTO<GetProductsDTO>> GetProductsPageAsync(GetProductsPagePayload payload);
        Task AddNewProductAsync(AddProductPayload payload);

    }
}
