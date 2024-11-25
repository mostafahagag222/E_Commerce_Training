using E_Commerce1DB_V01.DTOs;
using Microsoft.EntityFrameworkCore;
using E_Commerce1DB_V01.Entities;
using E_Commerce1DB_V01.Repositories.Interfaces;

namespace E_Commerce1DB_V01.Repositories
{
    public class BasketRepository : GenericRepository<Basket>, IBasketRepository
    {
        public ECPContext EcpContext => context;
        private readonly ECPContext context;

        public BasketRepository(ECPContext context) : base(context)
        {
            this.context = context;
        }
        public async Task AddDeliveryMethodAsync(string id, int deliveryMethodId)
        {
            await context.Baskets.Where(c => c.Id == id).ExecuteUpdateAsync(c => c.SetProperty(c => c.ShippingMethodID ,deliveryMethodId.ToString()));
        }
        public async Task AddGuidToBasket(string basketId, string gUID)
        {
            await context.Baskets.Where(c => c.Id == basketId)
                .ExecuteUpdateAsync(c => c
                .SetProperty(c => c.GUID , gUID));
        }
        public async Task<bool> DoesBasketExist(string id)
        {
            return await context.Baskets.AnyAsync(c => c.Id == id);
        }
        public async Task DeleteBasketAsync(string id)
        {
            var Basket = await context.Baskets.FirstOrDefaultAsync(c => c.Id == id);
            context.Remove(Basket);
        }
        public async Task<GetPaymentAmountDTO> GetProductAndBasketItemPrices(string basketId)
        {
            var result = await (from c in context.Baskets
                                where c.Id == basketId
                                select new GetPaymentAmountDTO
                                {
                                    ShippingPrice = c.ShippingMethod.Price,
                                    BasketItemsWithProductPrices = c.BasketItems.Select(ci=>new GetItemPriceDetailsDTO()
                                    {
                                        BasketItemPrice = ci.Price,
                                        ProductId = ci.ProductID,
                                        ProductPrice = ci.Product.Price,
                                        Quantity = ci.Quantity,
                                        TotalPrice = ci.TotalPrice
                                    }).ToList()
                                }
                                ).FirstOrDefaultAsync();
            return result;
        }
        public async Task<ShippingMethodIdAndSubtotalDTO> GetSMIdAndSubTotalAsync(string basketId)
        {
            var result = await (from c in context.Baskets
                                where c.Id == basketId
                                select new ShippingMethodIdAndSubtotalDTO()
                                {
                                    ShippingMethodId = c.ShippingMethodID,
                                    Subtotal = c.BasketItems.Sum(ci => ci.Quantity * ci.Quantity)
                                }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> UpdateBasketAfterAddingBasketItemAsync(string basketID, decimal itemPrice)
        {
            return await context
                .Baskets
                .Where(c => c.Id == basketID)
                .ExecuteUpdateAsync(c => c
                .SetProperty(c => c.UpdatedDate, DateTime.Now)
                .SetProperty(c => c.TotalPrice, c => c.TotalPrice + itemPrice)
                .SetProperty(c=>c.TotalQuantity, c=> c.TotalQuantity+1)
                ) > 0;
        }
        public async Task<bool> UpdateBasketAfterRemovingBasketItemAsync(string basketID, decimal itemPrice)
        {
            return await context
                .Baskets
                .Where(c => c.Id == basketID)
                .ExecuteUpdateAsync(c => c
                .SetProperty(c => c.UpdatedDate, DateTime.Now)
                .SetProperty(c => c.TotalPrice, c => c.TotalPrice - itemPrice)
                .SetProperty(c=>c.TotalQuantity, c=> c.TotalQuantity-1)
                ) > 0;
        }
    }
}
