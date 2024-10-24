using Azure;
using E_Commerce1DB_V01;
using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Entities;
using E_Commerce2Business_V01.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce2Business_V01.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public PaymentService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<RedirectionUrlDTO> CreatePaymentRequest(string basketId, int userId)
        {
            var paymentAmount = await GetPaymentAmount(basketId);
            var returnUrl = _configuration["PaymentRequestData:ReturnUrl"];
            var cancelUrl = _configuration["PaymentRequestData:CancelUrl"];
            var notificationUrl = _configuration["PaymentRequestData:NotificationUrl"];
            var authorizationValue = _configuration["PaymentRequestData:AuthorizationValue"];
            var upaymentsRequestAPI = _configuration["PaymentRequestData:UpaymentRequestAPI"];
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization",authorizationValue);
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            var requestBody = new PaymentRequestDTO()
            {
                CancelUrl = cancelUrl,
                NotificationUrl = notificationUrl,
                ReturnUrl = returnUrl,
                PaymentGateway = new PaymentGatewayDTO(),
                Customer = new CustomerDTO()
                {
                    Email = user.Email,
                    Name = user.Name,
                    UniqueId = userId.ToString()
                },
                Order = new PaymentOrderDTO()
                {
                    Amount = paymentAmount,
                    Id = Guid.NewGuid().ToString()
                },
                Reference = new ReferenceDTO(),
            };
            var requestBodyToJson = JsonConvert.SerializeObject(requestBody);
            var requestBodyContent = new StringContent(requestBodyToJson, Encoding.UTF8,"application/json");
            var response = await httpClient.PostAsync(upaymentsRequestAPI, requestBodyContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                // Log the status code and response content for better debugging
                Console.WriteLine($"Error: {response.StatusCode}, Content: {responseContent}");
            }
            response.EnsureSuccessStatusCode();
            var responseObject = JsonConvert.DeserializeObject<UPaymentsApiResponseDTO>(responseContent);
            return new RedirectionUrlDTO()
            {
                threeDSecureUrl = responseObject.Data.Link
            };
        }
        private async Task<Decimal> GetPaymentAmount(string cartId)
        {
            List<GetPaymentAmountDTO> items = await _unitOfWork.CartRepository.GetProductAndCartItemPrices(cartId);
            var ChangedPricesProductsIds = new List<int>();
            foreach (var item in items)
            {
                if (item.ProductPrice != item.CartItemPrice)
                    ChangedPricesProductsIds.Add(item.ProductId);
            }
            var number = ChangedPricesProductsIds.Count;
            if (number > 0)
                throw new ConflictException($"{number} cart items prices changed");
            return items.Sum(i => i.CartItemPrice);
        }
    }
}
