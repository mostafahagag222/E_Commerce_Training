using E_Commerce1DB_V01.DTOs;
using E_Commerce2Business_V01.Payloads;

namespace E_Commerce2Business_V01.Interfaces
{
    public interface IBasketService
    {
        Task DeleteBasketAsync(string id);
        Task<BasketDTO> GetBasketDetailsAsync(string basketId);
        Task<BasketDTO> UpdateBasketAsync(UpdateBasketPayload payload);
    }
}
