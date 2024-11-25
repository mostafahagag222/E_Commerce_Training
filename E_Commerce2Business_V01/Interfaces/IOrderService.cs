using E_Commerce1DB_V01.DTOs;
using Microsoft.AspNetCore.Http;

namespace E_Commerce2Business_V01.Interfaces
{
    public interface IOrderService
    {
        Task<List<GetAllOrdersDTO>> GetAllOrdersForUser(int userId);
        Task<OrderDTO> GetOrderDetailsById(int orderId);
        Task HandlePaymentResultAsync(IFormCollection formData);
    }
}
