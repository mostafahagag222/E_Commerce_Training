using E_Commerce1DB_V01;
using E_Commerce1DB_V01.DTOs;
using E_Commerce2Business_V01.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce2Business_V01.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateOrderAsync(string basketId)
        {
            /*
             *get list of cart items dto
             *get shipping price
             *dictionary of prices key have product price value have cart item price 
             *call it OrderDTO
             */
            var order = new Order()
            {
                CartId = basketId,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                TotalQuantity =0,
                TotalPrice = 0,
                OrderStatus = OrderStatus.PendingPayment
            };
            await _unitOfWork.OrderRepository.AddAsync(order);
            if (await _unitOfWork.SaveChangesAsync() < 1)
                throw new InternalServerErrorException("couldn't crate order");
            await _unitOfWork.OrderItemRepository.TransferCartItemsToOrderItemsAsync(basketId);
            if (await _unitOfWork.SaveChangesAsync() < 1)
                throw new InternalServerErrorException("something went wrong");

        }

        public Task<RedirectionUrlDTO> CreatePaymentRequest(string basketId)
        {
            throw new NotImplementedException();
        }
    }
}
