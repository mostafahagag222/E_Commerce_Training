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
            var orderDTO = await (from ci in context.CartItems
                                                     where ci.CartID == basketId
                                                     join p in context.Products
                                                     on ci.ProductID equals p.Id
                                                     join sm in context.ShippingMethods
                                                     on ci.Cart.ShippingMethodID equals sm.Id
                                                     select new OrderDTO
                                                     {
                                                         Items = new CartItemDTO()
                                                         {
                                                             CurrentPrice = p.Price,
                                                             ProductId = p.Id,
                                                             Quantity = ci.Quantity,
                                                             TotalPrice = ci.Quantity * p.Price,
                                                             OrderId = ci.Cart.Order.Id
                                                         },
                                                         ShippingFees = ci.Cart.ShippingMethod.Price
                                                     }
                                                     ).FirstOrDefaultAsync();
            var OrderDTO2 = await (from c in context.Carts
                                   where c.Id == basketId
                                   select new OrderDTO()
                                   {
                                       Items = c.CartItems.Select(ci => new CartItemDTO()
                                       {
                                           CurrentPrice=ci.Product.Price,
                                           ProductId = ci.ProductID,
                                           Quantity = ci.Quantity,
                                           TotalPrice = ci.Quantity * ci.Product.Price
                                       }).ToList(),
                                       ProductCartItemPrices = c.CartItems.ToDictionary(ci=>ci.Product.Price,ci=>ci.Price),
                                       ShippingPrice = c.ShippingMethod.Price
                                   }
                                 ).FirstOrDefaultAsync() ;
            var ToBeAddedOrderItems = orderDTO.items.Select(c => new OrderItem
            {
                OrderId = c.OrderId,
                Quantity = c.Quantity,
                TotalPrice = c.TotalPrice,
                ProductId = c.ProductId,
                Price = c.CurrentPrice,
                OrderTotalPrice = 0
            }).ToList();
            await context.OrderItems.AddRangeAsync(ToBeAddedOrderItems);
            var orderId = orderDTO.items.Select(c => c.ProductId).FirstOrDefault();
            var order = await context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            order.Updated = DateTime.Now;
            order.TotalPrice = ToBeAddedOrderItems.Sum(t => t.TotalPrice) + orderDTO.ShippingFees;
            order.TotalQuantity = ToBeAddedOrderItems.Sum(t => t.Quantity);
            context.Update(order);
            throw new Exception();
        }
    }
}
