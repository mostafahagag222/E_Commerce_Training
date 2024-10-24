using E_Commerce1DB_V01.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ECPContext context;
        public OrderRepository(ECPContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<AddOrderDTO> GetOrderDTOAsync(string basketId)
        {
            var OrderDTO = await (from c in context.Carts
                                  .Include(c => c.CartItems)
                                  .ThenInclude(ci => ci.Product)
                                  .Include(c => c.ShippingMethod)
                                  where c.Id == basketId
                                  select new AddOrderDTO()
                                  {
                                      Items = c.CartItems.Select(ci => new CartItemDTO()
                                      {
                                          ProductPrice = ci.Product.Price,
                                          ProductId = ci.ProductID,
                                          Quantity = ci.Quantity,
                                          TotalPrice = ci.Quantity * ci.Product.Price,
                                          CartItemPrice = ci.Price
                                      }).ToList(),
                                      ShippingPrice = c.ShippingMethod.Price
                                  }
                                 ).FirstOrDefaultAsync();
            return OrderDTO;
        }
    }
}
