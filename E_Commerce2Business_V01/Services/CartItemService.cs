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
    public class CartItemService : ICartItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;
        public CartItemService(IUnitOfWork unitOfWork, IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
        }
        public async Task SyncCartItemsAsync(string cartId, List<ProductPayload> productPayloads)
        {
            // Fetch existing CartItems from the database
            List<CartItem> existingCartItems = await _unitOfWork.CartItemRepository.GetCartItemsFroSpecificCart(cartId);
            List<CartItem> incomingCartItems = MapIncomingPayloadCartItemsToCartItemsAsync(cartId, productPayloads);

            // 1. Update or add new items
            foreach (var incomingCartItem in incomingCartItems)
            {
                var unitsInStockForThisProduct = await _unitOfWork.ProductRepository.GetUnitsInStockForOneProductAsync(incomingCartItem.ProductID);
                var existingCartItem = existingCartItems
                    .FirstOrDefault(c => c.ProductID == incomingCartItem.ProductID && c.CartID == incomingCartItem.CartID);
                if (existingCartItem != null)
                {
                    await UpdateExistingCartItem(unitsInStockForThisProduct, incomingCartItem, existingCartItem, cartId);
                }
                else
                {
                    await CreateCartItem(unitsInStockForThisProduct, incomingCartItem, cartId);
                }
            }
            // 2. Identify and remove CartItems not present in the incoming list
            var incomingProductIds = incomingCartItems.Select(c => c.ProductID).ToHashSet();
            var cartItemsToRemove = existingCartItems.Where(c => !incomingProductIds.Contains(c.ProductID)).ToList();
            if (cartItemsToRemove.Any())
            {
                //_context.Products.RemoveRange(productsToRemove);
                _unitOfWork.CartItemRepository.DeleteRange(cartItemsToRemove);
                await _unitOfWork.CartRepository.UpdateCartAfterRemovingCartItemAsync(cartId, cartItemsToRemove.FirstOrDefault().Price);
            }
            // Save all changes at once
            await _unitOfWork.SaveChangesAsync();
        }
        private List<CartItem> MapIncomingPayloadCartItemsToCartItemsAsync(string cartId, List<ProductPayload> productPayloads)
        {
            // Map incoming payloads to entities
            return productPayloads.Select(p => new CartItem
            {
                Price = p.Price,
                CartID = cartId,
                ProductID = p.Id,
                Quantity = p.Quantity
            }).ToList();
        }
        private async Task CreateCartItem(int unitsInStockForThisProduct, CartItem incomingCartItem, string cartId)
        {
            if (unitsInStockForThisProduct > 0)
            {
                // Add new CartItem
                await _unitOfWork.CartItemRepository.AddAsync(incomingCartItem);
                await _unitOfWork.CartRepository.UpdateCartAfterAddingCartItemAsync(cartId, incomingCartItem.Price);
            }
        }
        private async Task UpdateExistingCartItem(int unitsInStockForThisProduct, CartItem incomingCartItem, CartItem existingCartItem, string cartId)
        {
            if (unitsInStockForThisProduct >= incomingCartItem.Quantity)
            {
                if (incomingCartItem.Quantity > existingCartItem.Quantity)
                    await _unitOfWork.CartRepository.UpdateCartAfterAddingCartItemAsync(cartId, incomingCartItem.Price);
                if (incomingCartItem.Quantity < existingCartItem.Quantity)
                    await _unitOfWork.CartRepository.UpdateCartAfterRemovingCartItemAsync(cartId, incomingCartItem.Price);
                // Update existing product
                existingCartItem.Quantity = incomingCartItem.Quantity;
                existingCartItem.Price = incomingCartItem.Price;
            }
        }

        #region Old Services
        #region MyRegion
        //public async Task AddItemsToCartItemsAsync(string cartId, List<ProductPayload> toBeAdded)
        //{
        //    //loop items sent from the request
        //    foreach (var CIPL in toBeAdded)
        //    {
        //        //if cart item exist for this cart update if cart item doesn't exist create
        //        CartItem cartItem = await _unitOfWork.CartItemRepository.GetCartItemByCartIdProductID(cartId, CIPL.Id);
        //        if (cartItem != null)
        //        {
        //            await UpdateCartItem(cartItem);
        //        }
        //        else
        //        {
        //            await CreateCartItem(cartId, CIPL.Id, CIPL.Price);
        //        }
        //        //update cart details after adding item to the cart
        //        await UpdateCartAfterAddingCartItemAsync(cartId, CIPL.Price);
        //    }
        //} 
        #endregion
        #region MyRegion
        //private async Task UpdateCartItem(CartItem cartItem)
        //{
        //    var quis = await _unitOfWork.CartItemRepository.GetCartItemQuantityAndUnitInStockAsync(cartItem.CartID, cartItem.Id);
        //    if ((quis.ProdutcStockQuantity - quis.CartItemQuantity) < 1)
        //        throw new ConflictException("out of stock");
        //    cartItem.Quantity += 1;
        //    _unitOfWork.CartItemRepository.UpdateAsync(cartItem);
        //    await _unitOfWork.SaveChangesAsync();
        //} 
        #endregion
        #region MyRegion
        //private async Task CreateCartItem(string cartId, int productId, decimal price)
        //{
        //    var uis = await _productService.GetUnitsInStockForOneProductAsync(productId);
        //    if (uis < 1)
        //        throw new ConflictException("out of stock");
        //    var cartItem = new CartItem()
        //    {
        //        CartID = cartId,
        //        Price = price,
        //        ProductID = productId,
        //        Quantity = 1
        //    };
        //    await _unitOfWork.CartItemRepository.AddAsync(cartItem);
        //    await _unitOfWork.SaveChangesAsync();
        //} 
        #endregion
        #region MyRegion
        //public async Task<bool> UpdateCartAfterAddingCartItemAsync(string cartID, decimal unitPrice)
        //{
        //    return await _unitOfWork.CartRepository.UpdateCartAfterAddingCartItemAsync(cartID, unitPrice);
        //} 
        #endregion
        #endregion

    }
}
