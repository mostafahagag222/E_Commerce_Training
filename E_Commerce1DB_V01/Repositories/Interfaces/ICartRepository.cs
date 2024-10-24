using E_Commerce1DB_V01.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.Repositories
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task AddDeliveryMethodAsync(string id, int deliveryMethodId);
        Task<bool> CheckExistenceByID(string id);
        Task DeleteCartAsync(string id);
        Task<List<(decimal ProductPrice, decimal CartItemPrice , int ProductId)>> GetProductAndCartItemPrices(string cartId);
        Task<bool> UpdateCartAfterAddingCartItemAsync(string cartID, decimal itemPrice);
        Task<bool> UpdateCartAfterRemovingCartItemAsync(string cartID, decimal itemPrice);
    }
}
