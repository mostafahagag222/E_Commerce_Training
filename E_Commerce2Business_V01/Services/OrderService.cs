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
             * create order
             * fetch basket items in basketitemdto{including price from product}
             * map them into order items
             * add them to order items
             * update order after each add
             * convert basket items to order items with product current price
             * give them the created orderid
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
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.OrderRepository.AddAsync(order);
            await _unitOfWork.OrderItemRepository.TransferCartItemsToOrderItemsAsync(basketId);
            await _unitOfWork.CommitTransactionAsync();
            
        }

        public Task<RedirectionUrlDTO> CreatePaymentRequest(string basketId)
        {
            throw new NotImplementedException();
        }
    }
}
