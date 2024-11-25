using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Entities;

namespace E_Commerce1DB_V01.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<GetAllOrdersDTO>> GetAllOrdersDTO(int userId);
        Task<OrderDTO> GetOrderDetailsById(int orderId);
        Task<int> GetOrderId(string basketId);
    }
}
