using E_Commerce1DB_V01.DTOs;

namespace E_Commerce2Business_V01.Interfaces
{
    public interface IShippingMethodService
    {
        Task<List<DeliveryMethodDTO>> GetDeliveryMethodsDTOAsync();
    }
}
