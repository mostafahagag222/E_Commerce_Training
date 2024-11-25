using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Entities;

namespace E_Commerce1DB_V01.Repositories.Interfaces
{
    public interface IShippingMethodRepository : IGenericRepository<ShippingMethod>
    {
        Task<List<DeliveryMethodDTO>> GetDeliveryMethodsDTOAsync();
    }
}
