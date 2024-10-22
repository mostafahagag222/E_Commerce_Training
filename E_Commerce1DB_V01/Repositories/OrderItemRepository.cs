using E_Commerce1DB_V01.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.Repositories
{
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        private readonly ECPContext context;
        public OrderItemRepository(ECPContext context) : base(context)
        {
            this.context = context;
        }

        public async Task TransferCartItemsToOrderItemsAsync(string basketId)
        {
            /*
 * fetch basket items in basketitemdto{including price from product} 11
 * map them into order items
 * add them to order items
 * update order after each add
 * convert basket items to order items with product current price
 * give them the created orderid
 */
            var CartItemsToBeTransferedDTOs = await (from ci in context.CartItems
                                                     where ci.CartID == basketId
                                                     join p in context.Products
                                                     on ci.ProductID equals p.Id
                                                     select new CartItemDTO()
                                                     {
                                                         CurrentPrice = p.Price,
                                                         ProductId = p.Id,
                                                         Quantity = ci.Quantity,
                                                         TotalPrice = ci.Quantity*p.Price,
                                                         OrderId = ci.Cart.Order.Id
                                                     }
                                                     ).ToListAsync();
            var ToBeAddedOrderItems = CartItemsToBeTransferedDTOs.Select(c => new OrderItem
            {
                OrderId = c.OrderId,
                Quantity = c.Quantity,
                TotalPrice = c.TotalPrice,
                ProductId = c.ProductId,
                Price = c.CurrentPrice,
                OrderTotalPrice = 0
            }).ToList();
            await context.OrderItems.AddRangeAsync(ToBeAddedOrderItems);
            var order = context.Orders.FirstOrDefaultAsync (o=>o.Id==ToBeAddedOrderItems.Select(t=>t.Id).FirstOrDefault());   
        }
    }
}
