using E_Commerce1DB_V01;
using E_Commerce2Business_V01.Interfaces;
using E_Commerce2Business_V01.Payloads;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce2Business_V01.Services
{
    public class BasketItemService : IBasketItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;
        public BasketItemService(IUnitOfWork unitOfWork, IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
        }
        public async Task SyncBasketItemsAsync(string basketId, List<BasketItemPayload> productPayloads)
        {
            // Fetch existing BasketItems from the database
            List<BasketItem> existingBasketItems = await _unitOfWork.BasketItemRepository.GetBasketItemsFroSpecificBasket(basketId);
            List<BasketItem> incomingBasketItems = MapIncomingPayloadBasketItemsToBasketItemsAsync(basketId, productPayloads);

            // 1. Update or add new items
            foreach (var incomingBasketItem in incomingBasketItems)
            {
                var unitsInStockForThisProduct = await _unitOfWork.ProductRepository.GetUnitsInStockForOneProductAsync(incomingBasketItem.ProductID);
                var existingBasketItem = existingBasketItems
                    .FirstOrDefault(c => c.ProductID == incomingBasketItem.ProductID && c.BasketID == incomingBasketItem.BasketID);
                if (existingBasketItem != null)
                {
                    await UpdateExistingBasketItem(unitsInStockForThisProduct, incomingBasketItem, existingBasketItem, basketId);
                }
                else
                {
                    await CreateBasketItem(unitsInStockForThisProduct, incomingBasketItem, basketId);
                }
            }
            // 2. Identify and remove BasketItems not present in the incoming list
            var incomingProductIds = incomingBasketItems.Select(c => c.ProductID).ToHashSet();
            var basketItemsToRemove = existingBasketItems.Where(c => !incomingProductIds.Contains(c.ProductID)).ToList();
            if (basketItemsToRemove.Any())
            {
                //_context.Products.RemoveRange(productsToRemove);
                _unitOfWork.BasketItemRepository.DeleteRange(basketItemsToRemove);
                await _unitOfWork.BasketRepository.UpdateBasketAfterRemovingBasketItemAsync(basketId, basketItemsToRemove.FirstOrDefault().Price);
            }
            // Save all changes at once
            await _unitOfWork.SaveChangesAsync();
        }
        private List<BasketItem> MapIncomingPayloadBasketItemsToBasketItemsAsync(string basketId, List<BasketItemPayload> productPayloads)
        {
            // Map incoming payloads to entities
            return productPayloads.Select(p => new BasketItem
            {
                Price = p.Price,
                BasketID = basketId,
                ProductID = p.Id,
                Quantity = p.Quantity
            }).ToList();
        }
        private async Task CreateBasketItem(int unitsInStockForThisProduct, BasketItem incomingBasketItem, string basketId)
        {
            if (unitsInStockForThisProduct > 0)
            {
                // Add new BasketItem
                await _unitOfWork.BasketItemRepository.AddAsync(incomingBasketItem);
                await _unitOfWork.BasketRepository.UpdateBasketAfterAddingBasketItemAsync(basketId, incomingBasketItem.Price);
            }
        }
        private async Task UpdateExistingBasketItem(int unitsInStockForThisProduct, BasketItem incomingBasketItem, BasketItem existingBasketItem, string basketId)
        {
            if (unitsInStockForThisProduct >= incomingBasketItem.Quantity)
            {
                if (incomingBasketItem.Quantity > existingBasketItem.Quantity)
                    await _unitOfWork.BasketRepository.UpdateBasketAfterAddingBasketItemAsync(basketId, incomingBasketItem.Price);
                if (incomingBasketItem.Quantity < existingBasketItem.Quantity)
                    await _unitOfWork.BasketRepository.UpdateBasketAfterRemovingBasketItemAsync(basketId, incomingBasketItem.Price);
                // Update existing product
                existingBasketItem.Quantity = incomingBasketItem.Quantity;
                existingBasketItem.Price = incomingBasketItem.Price;
            }
        }

        #region Old Services
        #region MyRegion
        //public async Task AddItemsToBasketItemsAsync(string basketId, List<ProductPayload> toBeAdded)
        //{
        //    //loop items sent from the request
        //    foreach (var CIPL in toBeAdded)
        //    {
        //        //if Basket item exist for this Basket update if Basket item doesn't exist create
        //        BasketItem basketItem = await _unitOfWork.BasketItemRepository.GetBasketItemByBasketIdProductID(basketId, CIPL.Id);
        //        if (basketItem != null)
        //        {
        //            await UpdateBasketItem(basketItem);
        //        }
        //        else
        //        {
        //            await CreateBasketItem(basketId, CIPL.Id, CIPL.Price);
        //        }
        //        //update Basket details after adding item to the Basket
        //        await UpdateBasketAfterAddingBasketItemAsync(basketId, CIPL.Price);
        //    }
        //} 
        #endregion
        #region MyRegion
        //private async Task UpdateBasketItem(BasketItem basketItem)
        //{
        //    var quis = await _unitOfWork.BasketItemRepository.GetBasketItemQuantityAndUnitInStockAsync(basketItem.BasketID, basketItem.Id);
        //    if ((quis.ProdutcStockQuantity - quis.BasketItemQuantity) < 1)
        //        throw new ConflictException("out of stock");
        //    basketItem.Quantity += 1;
        //    _unitOfWork.BasketItemRepository.UpdateAsync(basketItem);
        //    await _unitOfWork.SaveChangesAsync();
        //} 
        #endregion
        #region MyRegion
        //private async Task CreateBasketItem(string basketId, int productId, decimal price)
        //{
        //    var uis = await _productService.GetUnitsInStockForOneProductAsync(productId);
        //    if (uis < 1)
        //        throw new ConflictException("out of stock");
        //    var basketItem = new BasketItem()
        //    {
        //        BasketID = basketId,
        //        Price = price,
        //        ProductID = productId,
        //        Quantity = 1
        //    };
        //    await _unitOfWork.BasketItemRepository.AddAsync(basketItem);
        //    await _unitOfWork.SaveChangesAsync();
        //} 
        #endregion
        #region MyRegion
        //public async Task<bool> UpdateBasketAfterAddingBasketItemAsync(string basketID, decimal unitPrice)
        //{
        //    return await _unitOfWork.BasketRepository.UpdateBasketAfterAddingBasketItemAsync(basketID, unitPrice);
        //} 
        #endregion
        #endregion

    }
}
