using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Entities;

namespace E_Commerce1DB_V01.Repositories.Interfaces
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
