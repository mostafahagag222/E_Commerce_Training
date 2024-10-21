using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        private readonly ECPContext context;

        public CartRepository(ECPContext context) : base(context)
        {
            this.context = context;
        }

        public async Task AddDeliveryMethodAsync(string id, int deliveryMethodId)
        {
            await context.Carts.Where(c => c.Id == id).ExecuteUpdateAsync(c => c.SetProperty(c => c.ShippingMethodID ,deliveryMethodId.ToString()));
        }
        public async Task<bool> CheckExistenceByID(string id)
        {
            return await context.Carts.AnyAsync(c => c.Id == id);
        }
        public async Task DeleteCartAsync(string id)
        {
            var cart = await context.Carts.FirstOrDefaultAsync(c => c.Id == id);
            context.Remove(cart);
        }

        public async Task<bool> UpdateCartAfterAddingCartItemAsync(string cartID, decimal itemPrice)
        {
            return await context
                .Carts
                .Where(c => c.Id == cartID)
                .ExecuteUpdateAsync(c => c
                .SetProperty(c => c.UpdatedDate, DateTime.Now)
                .SetProperty(c => c.TotalPrice, c => c.TotalPrice + itemPrice)
                .SetProperty(c=>c.TotalQuantity, c=> c.TotalQuantity+1)
                ) > 0;
        }
        public async Task<bool> UpdateCartAfterRemovingCartItemAsync(string cartID, decimal itemPrice)
        {
            return await context
                .Carts
                .Where(c => c.Id == cartID)
                .ExecuteUpdateAsync(c => c
                .SetProperty(c => c.UpdatedDate, DateTime.Now)
                .SetProperty(c => c.TotalPrice, c => c.TotalPrice - itemPrice)
                .SetProperty(c=>c.TotalQuantity, c=> c.TotalQuantity-1)
                ) > 0;
        }
    }
}
