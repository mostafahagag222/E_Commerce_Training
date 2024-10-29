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
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task AddDeliveryMethodAsync(string id, int deliveryMethodId);
        Task AddGuidToCart(string basketId, string gUID);
        Task<bool> CheckExistenceByID(string id);
        Task DeleteCartAsync(string id);
        Task<GetPaymentAmountDTO> GetProductAndCartItemPrices(string cartId);
        Task<ShippingMethodIdAndSubtotalDTO> GetSMIdAndSubTotalAsync(string basketId);
        Task<bool> UpdateCartAfterAddingCartItemAsync(string cartID, decimal itemPrice);
        Task<bool> UpdateCartAfterRemovingCartItemAsync(string cartID, decimal itemPrice);
    }
}
