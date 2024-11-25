using E_Commerce1DB_V01;
using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Entities;
using E_Commerce2Business_V01.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using E_Commerce2Business_V01.Exceptions;

namespace E_Commerce2Business_V01.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<GetAllOrdersDTO>> GetAllOrdersForUser(int userId)
        {
            var orders = (await _unitOfWork.OrderRepository.GetAllOrdersDTO(userId));
            return orders;
        }

        public async Task<OrderDTO> GetOrderDetailsById(int orderId)
        {
            return await _unitOfWork.OrderRepository.GetOrderDetailsById(orderId);
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
                await CreateOrderAsync(payment.BasketId, (int)payment.UserId, payment.Amount);
                await _unitOfWork.SaveChangesAsync();///////-----------------------------------
                int orderId = await _unitOfWork.OrderRepository.GetOrderId(payment.BasketId);
                await _unitOfWork.PaymentRepository.UpdateOrderId(orderId,response.GUID); // no save changes
                await _unitOfWork.BasketRepository.DeleteBasketAsync(payment.BasketId);
                var saveChanges = await _unitOfWork.SaveChangesAsync();
                if (saveChanges < 1)
                    throw new InternalServerErrorException("something went wrong during handling payment");
            }
        }
        private async Task CreateOrderAsync(string basketId, int userID, decimal amount)
        {
            /*
             * GET Basket ITEMS DATA
             * MAP Basket ITEMS DTOS TO ORDER ITEMS
             * CREATE NEW ORDER WITH ORDER ITEMS INCLUDED
             * ADD ORDER ITEM WITH EF
             * UPDATE FETCHED PRODUCTS STOCK
             */
            var items = await _unitOfWork.BasketItemRepository.GetBasketItemsDTOAsync(basketId); // 2
            var OrderItems = items.Select(i => new OrderItem()
            {
                Price = i.Price,
                Quantity = i.Quantity,
                ProductId = i.Product.Id,
                TotalPrice = i.TotalPrice
            }).ToList();
            ShippingMethodIdAndSubtotalDTO shippingMethodIdAndSubtotalDTO = await _unitOfWork.BasketRepository.GetSMIdAndSubTotalAsync(basketId);
            var order = new Order()
            {
                UserId = userID,
                OrderItems = OrderItems,
                BasketId = basketId,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                TotalQuantity = OrderItems.Sum(o => o.Quantity),
                TotalPrice = amount,
                ShippingMethodId = shippingMethodIdAndSubtotalDTO.ShippingMethodId,
                SubTotal = shippingMethodIdAndSubtotalDTO.Subtotal,
                OrderStatus = OrderStatus.PendingDelivery,
            };
            await _unitOfWork.OrderRepository.AddAsync(order); // no save changes
            UpdateProductsStockAsync(items); // no save changes
        }
        private void UpdateProductsStockAsync(List<CreateOrderItemDTO> items)
        {
            foreach (var basketItem in items)
            {
                basketItem.Product.UnitsInStock -= basketItem.Quantity;
                _unitOfWork.ProductRepository.UpdateAsync(basketItem.Product);
            }
        }
    }
}
