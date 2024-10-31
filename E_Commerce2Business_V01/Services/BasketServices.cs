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
    public class BasketService : IBasketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketItemService _basketItemService;

        public BasketService(IUnitOfWork unitOfWork, IBasketItemService basketItemService)
        {
            _unitOfWork = unitOfWork;
            _basketItemService = basketItemService;
        }
        public async Task<BasketDTO> UpdateBasketAsync(UpdateBasketPayload updateBasketPayload)
        {
            /*
             * if basket doesn't already exist create new one with the string id sent
             */
            var doesBasketExist = await _unitOfWork.BasketRepository.DoesBasketExist(updateBasketPayload.Id);
            if (!doesBasketExist)
            {
                await CreateBasketAsync(updateBasketPayload.Id);
                var saveChanges = await _unitOfWork.SaveChangesAsync();
                if (saveChanges < 1)
                    throw new InternalServerErrorException("couldn't create Basket");
            }
            await _basketItemService.SyncBasketItemsAsync(updateBasketPayload.Id, updateBasketPayload.Items);
            if (updateBasketPayload.DeliveryMethodId != 0)
            {
                await _unitOfWork.BasketRepository.AddDeliveryMethodAsync(updateBasketPayload.Id, updateBasketPayload.DeliveryMethodId);
            }
            return await GetBasketDetailsAsync(updateBasketPayload.Id);
        }
        private async Task CreateBasketAsync(string id)
        {
            var toBeAddedBasket = new Basket()
            {
                Id = id,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                TotalPrice = 0,
                TotalQuantity = 0,
                
            };
            await _unitOfWork.BasketRepository.AddAsync(toBeAddedBasket);
        }
        public async Task DeleteBasketAsync(string id)
        {
            await _unitOfWork.BasketRepository.DeleteBasketAsync(id);
            if (await _unitOfWork.SaveChangesAsync() < 1)
                throw new InternalServerErrorException("couldn't remove Basket");
        }
        public async Task<BasketDTO> GetBasketDetailsAsync(string basketId)
        {
            if (!await _unitOfWork.BasketRepository.DoesBasketExist(basketId))
                return null;
            return await _unitOfWork.BasketItemRepository.GetBasketDTOAsync(basketId);
        }
    }
}
