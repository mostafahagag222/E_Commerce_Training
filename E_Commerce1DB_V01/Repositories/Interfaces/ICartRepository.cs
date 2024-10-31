using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Repositories.Interfaces;
using E_Commerce2Business_V01.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.Repositories
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
    }
}
