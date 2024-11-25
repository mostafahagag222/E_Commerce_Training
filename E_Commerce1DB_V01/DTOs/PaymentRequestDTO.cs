using Newtonsoft.Json;

namespace E_Commerce1DB_V01.DTOs
{
    public class PaymentRequestDTO
    {
        [JsonProperty("order")]
        public PaymentDataDTO PaymentDataDTO { get; set; }
        [JsonProperty("paymentGateway")]
        public PaymentGatewayDTO PaymentGateway { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; } = "en";
        [JsonProperty("customer")]
        public CustomerDTO Customer { get; set; }
        [JsonProperty("returnUrl")]
        public string ReturnUrl { get; set; }
        [JsonProperty("cancelUrl")]
        public string CancelUrl { get; set; }
        [JsonProperty("notificationUrl")]
        public string NotificationUrl { get; set; }
        [JsonProperty("reference")]
        public ReferenceDTO Reference { get; set; }
    }

    public class PaymentDataDTO
    {
        [JsonProperty("id")]
        public string GUID { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; } = "KWD";
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; } = "test description";
    }
    public class ReferenceDTO
    {
        [JsonProperty("id")]
        public string Id = "anyId";
    }

    public class PaymentGatewayDTO
    {
        [JsonProperty("src")]
        public string Src { get; set; } = "knet";
    }

    public class CustomerDTO
    {
        [JsonProperty("uniqueId")]
        public string UniqueId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }



}
