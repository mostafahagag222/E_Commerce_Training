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
    public interface IBasketItemRepository : IGenericRepository<BasketItem>
    {
        void DeleteRange(List<BasketItem> basketItemsToRemove);
        Task DeleteRangeByBasketId(string id);
        Task<BasketItem> GetBasketItemByBasketIdProductID(string basketId, int producId);
        Task<QuantityUnitsInStockDTO> GetBasketItemQuantityAndUnitInStockAsync(string BasketId, int basketItemId);
        Task<List<BasketItem>> GetBasketItemsFroSpecificBasket(string basketId);
        Task<BasketDTO> GetBasketDTOAsync(string basketID);
        Task<List<CreateOrderItemDTO>> GetBasketItemsDTOAsync(string basketId);
    }
}
