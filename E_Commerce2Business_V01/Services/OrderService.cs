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
             *call it AddOrderDTO
             */
            var OrderDTO = await _unitOfWork.OrderRepository.GetOrderDTOAsync(basketId);
            CheckPriceChanges(OrderDTO.Items);
            var OrderItems = OrderDTO.Items.Select(i => new OrderItem()
            {
                OrderTotalPrice=0,
                Price = i.ProductPrice,
                Quantity = i.Quantity,
                ProductId = i.ProductId,
                TotalPrice = i.TotalPrice
            }).ToList();
            var order = new Order()
            {
                OrderItems = OrderItems,
                CartId = basketId,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                TotalQuantity =OrderItems.Sum(o=>o.Quantity),
                TotalPrice = OrderItems.Sum(o=>o.TotalPrice)+OrderDTO.ShippingPrice,
                OrderStatus = OrderStatus.PendingPayment
            };
            await _unitOfWork.OrderRepository.AddAsync(order);
            if (await _unitOfWork.SaveChangesAsync() < 1)
                throw new InternalServerErrorException("couldn't crate order");
            //await _unitOfWork.OrderItemRepository.TransferCartItemsToOrderItemsAsync(basketId);
            //if (await _unitOfWork.SaveChangesAsync() < 1)
            //    throw new InternalServerErrorException("something went wrong");

        }

        private void CheckPriceChanges(List<CartItemDTO> items)
        {
            var ChangedPricesProductsIds = new List<int>();
            foreach (var item in items)
            {
                if (item.ProductPrice != item.CartItemPrice)
                    ChangedPricesProductsIds.Add(item.ProductId);
            }
            var number = ChangedPricesProductsIds.Count;
            if (number > 0)
                throw new ConflictException($"{number} cart items prices changed");
        }

    }
}
