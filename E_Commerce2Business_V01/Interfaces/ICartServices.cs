using E_Commerce1DB_V01;
using E_Commerce2Business_V01.Payloads;

namespace E_Commerce2Business_V01.Interfaces
{
    public interface ICartServices
    {
        Task DeleteCartAsync(string id);
        Task<BasketDTO> GetBasketDetailsAsync(string basketId);
        Task<BasketDTO> UpdateCartItems(AddToBasketPayload payload);
    }
}
