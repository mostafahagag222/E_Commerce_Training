using E_Commerce1DB_V01.DTOs;
using Microsoft.EntityFrameworkCore;
using E_Commerce1DB_V01.Entities;
using E_Commerce1DB_V01.Repositories.Interfaces;

namespace E_Commerce1DB_V01.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ECPContext context;
        public OrderRepository(ECPContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<GetAllOrdersDTO>> GetAllOrdersDTO(int userId)
        {
            var result = await context.Orders
                .Where(o => o.UserId == userId)
                .Select(o => new GetAllOrdersDTO()
                {
                    Id = o.Id,
                    OrderDate = o.Updated.ToString(),
                    Status = o.OrderStatus.ToString(),
                    Total = o.TotalPrice
                }).ToListAsync();
            return result;
        }

        public async Task<OrderDTO> GetOrderDetailsById(int orderId)
        {
            var order = await context.Orders
                .Where(o => o.Id == orderId)
                .Select(o => new OrderDTO()
                {
                    DeliveryMethod = o.ShippingMethod != null ? o.ShippingMethod.Name : "ass",
                    ShippingPrice = o.ShippingMethod != null ? o.ShippingMethod.Price : 12,
                    Subtotal = o.SubTotal ,
                    OrderDate = o.Updated.ToString(),
                    Id = o.Id,
                    Status = o.OrderStatus.ToString(),
                    Total = o.TotalPrice,
                    BuyerEmail = o.User.Email,
                    ShipAddress = o.User.Addresses.Select(a => new AddressDTO()
                    {
                        City = a.City,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        State = a.State,
                        Street = a.Street,
                        ZipCode = a.ZipCode
                    }).FirstOrDefault(),
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDTO()
                    {
                        PictureUrl = oi.Product.ImageUrl,
                        Price = oi.Product.Price,
                        ProductId = oi.ProductId,
                        ProductName = oi.Product.Name,
                        Quantity = oi.Quantity
                    }).ToList(),
                    
                }).FirstOrDefaultAsync();
            return order;
        }

        public async Task<int> GetOrderId(string basketId)
        {
            return await context.Orders.Where(o => o.BasketId == basketId).Select(o => o.Id).FirstOrDefaultAsync();
        }
    }
}
