using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Entities;

namespace E_Commerce1DB_V01.Repositories.Interfaces
{
    public interface IBasketRepository : IGenericRepository<Basket>
    {
        Task AddDeliveryMethodAsync(string id, int deliveryMethodId);
        Task AddGuidToBasket(string basketId, string gUID);
        Task<bool> DoesBasketExist(string id);
        Task DeleteBasketAsync(string id);
        Task<GetPaymentAmountDTO> GetProductAndBasketItemPrices(string basketId);
        Task<ShippingMethodIdAndSubtotalDTO> GetSMIdAndSubTotalAsync(string basketId);
        Task<bool> UpdateBasketAfterAddingBasketItemAsync(string basketID, decimal itemPrice);
        Task<bool> UpdateBasketAfterRemovingBasketItemAsync(string basketID, decimal itemPrice);
        ECPContext EcpContext { get;  }
    }
}
