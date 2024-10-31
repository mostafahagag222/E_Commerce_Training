namespace E_Commerce2Business_V01.Services
{
    public class HandlePaymentPaymentDataDTO
    {
        public int PaymentId { get; set; }
        public int? UserId { get; set; }
        public string BasketId { get; set; }
        public decimal Amount { get; set; }
    }
}