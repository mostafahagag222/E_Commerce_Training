using E_Commerce1DB_V01;
using E_Commerce1DB_V01.DTOs;
using E_Commerce2Business_V01.Interfaces;
using E_Commerce2Business_V01.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce2Business_V01.Services
{
    public class CartServices : ICartServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartItemService _cartItemService;
        private readonly IProductService _productService;

        public CartServices(IUnitOfWork unitOfWork, ICartItemService cartItemService, IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _cartItemService = cartItemService;
            _productService = productService;
        }
        public async Task<BasketDTO> UpdateCartItems(AddToBasketPayload payload)
        {
            if (!await _unitOfWork.CartRepository.CheckExistenceByID(payload.Id))
            {
                // if doesn't exist create new one
                if (!await CreateCartAsync(payload.Id))
                    throw new InternalServerErrorException("couldn't create cart");
            }
            await _cartItemService.SyncCartItemsAsync(payload.Id, payload.Items);
            if (payload.DeliveryMethodId !=0)
            {
                await _unitOfWork.CartRepository.AddDeliveryMethodAsync(payload.Id, payload.DeliveryMethodId);
            }
            return await GetBasketDetailsAsync(payload.Id);
        }
        private async Task<bool> CreateCartAsync(string id)
        {
            await _unitOfWork.CartRepository.AddAsync(
                new Cart()
                {
                    Id = id,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    TotalPrice = 0,
                    TotalQuantity = 0
                });
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
        public async Task DeleteCartAsync(string id)
        {
            await _unitOfWork.CartItemRepository.DeleteRangeByCartId(id);
            if (await _unitOfWork.SaveChangesAsync() < 1)
                throw new InternalServerErrorException("couldn't remove cart Items");
            await _unitOfWork.CartRepository.DeleteCartAsync(id);
            if (await _unitOfWork.SaveChangesAsync() < 1)
                throw new InternalServerErrorException("couldn't remove cart");
        }
        public async Task<BasketDTO> GetBasketDetailsAsync(string basketId)
        {
            if (!await _unitOfWork.CartRepository.CheckExistenceByID(basketId))
                return null;
            return await _unitOfWork.CartItemRepository.GetBasketDTOAsync(basketId);
        }


        //public async Task UpdateCartItems(AddToBasketPayload payload)
        //{
        //    //initialize ToBeAdded and ToBeDeleted Lists
        //    List<ProductPayload> ToBeAdded = new List<ProductPayload>();
        //    List<ProductPayload> ToBeDeleted = new List<ProductPayload>();
        //    //check if cart exists 
        //    if (!await _unitOfWork.CartRepository.CheckExistenceByID(payload.Id))
        //    {
        //        // if doesn't exist create new one
        //        if (!await CreateCartAsync(payload.Id))
        //            throw new InternalServerErrorException("couldn't create cart");
        //        ToBeAdded.AddRange(payload.Items);
        //    }
        //    else
        //    {
        //        ToBeAdded.AddRange(await _cartItemService.DetectToBeAddedItems(payload.Id,payload.Items));
        //        ToBeDeleted.AddRange(await _cartItemService.DetectToBeDeletedItems(payload.Id,payload.Items));
        //    }
        //    if (ToBeAdded.Count>0)
        //    {
        //        await _cartItemService.AddItemsToCartItemsAsync(payload.Id, ToBeDeleted);
        //    }
        //    if (ToBeDeleted.Count>0)
        //    {
        //        await _cartItemService.DeleteItemsFromCartItemsAsync(payload.Id, ToBeDeleted);
        //    }

        //}
        //public async Task<BasketDTO> AddToCartItems(string cartId, List<ProductPayload> toBeDeleted)
        //{
        //    //add items to cart
        //    await _cartItemService.AddItemsToCartItemsAsync(payload);
        //    //return cart with its itmes in the response body
        //    return new BasketDTO()
        //    {
        //        Id = payload.Id,
        //        DeliveryMethodId = payload.DeliveryMethodId,
        //        ShippingPrice = payload.ShippingPrice,
        //        Items = await GetBasketDTOAsync(payload.Id)
        //    };

        //}
    }
}
