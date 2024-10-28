using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Repositories.Interfaces;
using E_Commerce2Business_V01.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.Repositories
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        void DeleteRange(List<CartItem> cartItemsToRemove);
        Task DeleteRangeByCartId(string id);
        Task<CartItem> GetCartItemByCartIdProductID(string cartId, int producId);
        Task<QuantityUnitsInStockDTO> GetCartItemQuantityAndUnitInStockAsync(string CartId, int cartItemId);
        Task<List<CartItem>> GetCartItemsFroSpecificCart(string cartId);
        Task<BasketDTO> GetBasketDTOAsync(string cartID);
        Task<List<CreateOrderItemDTO>> GetCartItemsDTOAsync(string basketId);
    }
}
