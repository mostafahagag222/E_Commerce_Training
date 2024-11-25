using E_Commerce1DB_V01;
using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Entities;
using E_Commerce2Business_V01.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using E_Commerce2Business_V01.Exceptions;

namespace E_Commerce2Business_V01.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentService> _logger;
        public PaymentService(IUnitOfWork unitOfWork, IConfiguration configuration, ILogger<PaymentService> logger)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<RedirectionUrlDTO> CreatePaymentRequest(string basketId, int userId)
        {
            using var httpClient = new HttpClient();
            var requestBodyAndAmount = await CreateRequestBody(basketId, userId, httpClient);
            var requestBodyToJson = JsonConvert.SerializeObject(requestBodyAndAmount.requestBody);
            var requestBodyContent = new StringContent(requestBodyToJson, Encoding.UTF8, "application/json");
            var upaymentsRequestAPI = _configuration["PaymentRequestData:UpaymentRequestAPI"];
            var response = await httpClient.PostAsync(upaymentsRequestAPI, requestBodyContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            var responseObject = JsonConvert.DeserializeObject<UPaymentsApiResponseDTO>(responseContent);
            await _unitOfWork.PaymentRepository.AddAsync(new Payment()
            {
                BasketId = basketId,
                Amount = requestBodyAndAmount.paymentAmount,
                CreatedAt = DateTime.Now,
                PaymentRequestUrl = responseObject.Data.Link,
                UniqueIdentifier = requestBodyAndAmount.requestBody.PaymentDataDTO.GUID,
                UpdatedAt = DateTime.Now,
                UserId = userId,
                Status = PaymentStatus.Pending,
                PaymentLogs = new List<PaymentLog>()
                {
                    new PaymentLog()
                    {
                    PaymentRequest = requestBodyToJson,
                    PaymentResponse = responseContent
                    }
                }
            });
            //add guid to Basket
            await _unitOfWork.BasketRepository.AddGuidToBasket(basketId,requestBodyAndAmount.requestBody.PaymentDataDTO.GUID);
            if (await _unitOfWork.SaveChangesAsync() < 1)
                throw new InternalServerErrorException("couldn't create Payment");
            return new RedirectionUrlDTO()
            {
                threeDSecureUrl = responseObject.Data.Link
            };
        }
        private async Task<Decimal> GetPaymentAmount(string basketId)
        {
            GetPaymentAmountDTO getPaymentAmountDTO = await _unitOfWork.BasketRepository.GetProductAndBasketItemPrices(basketId);
            var items = getPaymentAmountDTO.BasketItemsWithProductPrices;
            var ChangedPricesProductsIds = new List<int>();
            foreach (var item in items)
            {
                if (item.ProductPrice != item.BasketItemPrice)
                    ChangedPricesProductsIds.Add(item.ProductId);
            }
            var number = ChangedPricesProductsIds.Count;
            if (number > 0)
                throw new ConflictException($"{number} Basket items prices changed");
            return items.Sum(i => i.TotalPrice) + getPaymentAmountDTO.ShippingPrice;
        }
        private async Task<(PaymentRequestDTO requestBody, decimal paymentAmount)> CreateRequestBody(string basketId, int userId, HttpClient httpClient)
        {
            var paymentAmount = await GetPaymentAmount(basketId);
            var returnUrl = _configuration["PaymentRequestData:ReturnUrl"];
            var cancelUrl = _configuration["PaymentRequestData:CancelUrl"];
            var notificationUrl = _configuration["PaymentRequestData:NotificationUrl"];
            var authorizationValue = _configuration["PaymentRequestData:AuthorizationValue"];
            httpClient.DefaultRequestHeaders.Add("Authorization", authorizationValue);
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
                PaymentDataDTO = new PaymentDataDTO()
                {
                    Amount = paymentAmount,
                    GUID = Guid.NewGuid().ToString()
                },
                Reference = new ReferenceDTO(),
            };
            return (requestBody, paymentAmount);
        }
    }
}
