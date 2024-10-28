using E_Commerce1DB_V01;
using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Entities;
using E_Commerce2Business_V01.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client.AppConfig;
using Newtonsoft.Json;
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


        public async Task HandlePaymentResultAsync(IFormCollection formData)
        {
            // Serialize the form data directly
            string responseToJson = JsonConvert.SerializeObject(formData
                .ToDictionary(x => x.Key, x => x.Value.ToString()));
            var response = JsonConvert.DeserializeObject<UpaymentsWebHookResponseDTO>(responseToJson);
            if (response.Result != "CAPTURED")
            {
                await _unitOfWork.PaymentRepository.UpdateStatusAsync(response.GUID, PaymentStatus.FailedPayment);
                if (await _unitOfWork.SaveChangesAsync() < 1)
                    throw new InternalServerErrorException("couldn't change order status to Failed Payment");
                return;
            }
            else
            {
                /* update order status //
                 * create paymentlog and log response as string and add payment id //
                 * create order and order items and update stock
                 * remove basket and its items
                 */
                await _unitOfWork.PaymentRepository.UpdateStatusAsync(response.GUID, PaymentStatus.SuccessfulPayment); //no save changes
                HandlePaymentPaymentDataDTO payment = await _unitOfWork.PaymentRepository.GetHandlePaymentPaymentDataDTO(response.GUID); // 1 
                await _unitOfWork.PaymentLogRepository.AddAsync(new PaymentLog()
                {
                    PaymentId = payment.PaymentId,
                    PaymentResponse = responseToJson
                }); // no save changes
                await CreateOrderAsync(payment.CartId, (int)payment.UserId, payment.Amount);
                await _unitOfWork.SaveChangesAsync();///////-----------------------------------
                int orderId = await _unitOfWork.OrderRepository.GetOrderId(payment.CartId);
                await _unitOfWork.PaymentRepository.UpdateOrderId(orderId,response.GUID); // no save changes
                await _unitOfWork.CartRepository.DeleteCartAsync(payment.CartId);
                var saveChanges = await _unitOfWork.SaveChangesAsync();
                if (saveChanges < 1)
                    throw new InternalServerErrorException("something went wrong during handling payment");
            }
        }
        private async Task CreateOrderAsync(string basketId, int userID, decimal amount)
        {
            /*
             * GET CART ITEMS DATA
             * MAP CART ITEMS DTOS TO ORDER ITEMS
             * CREATE NEW ORDER WITH ORDER ITEMS INCLUDED
             * ADD ORDER ITEM WITH EF
             * UPDATE FETCHED PRODUCTS STOCK
             */
            var items = await _unitOfWork.CartItemRepository.GetCartItemsDTOAsync(basketId); // 2
            var OrderItems = items.Select(i => new OrderItem()
            {
                Price = i.Price,
                Quantity = i.Quantity,
                ProductId = i.Product.Id,
                TotalPrice = i.TotalPrice
            }).ToList();
            var order = new Order()
            {
                UserId = userID,
                OrderItems = OrderItems,
                CartId = basketId,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                TotalQuantity = OrderItems.Sum(o => o.Quantity),
                TotalPrice = amount
            };
            await _unitOfWork.OrderRepository.AddAsync(order); // no save changes
            UpdateProductsStockAsync(items); // no save changes
        }
        private void UpdateProductsStockAsync(List<CreateOrderItemDTO> items)
        {
            foreach (var cartItem in items)
            {
                cartItem.Product.UnitsInStock -= cartItem.Quantity;
                _unitOfWork.ProductRepository.UpdateAsync(cartItem.Product);
            }
        }
    }
}
