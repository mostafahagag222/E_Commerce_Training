namespace E_Commerce1DB_V01.DTOs
{
    public class HandlePaymentPaymentDataDTO
    {
        public int PaymentId { get; set; }
        public int? UserId { get; set; }
        public string BasketId { get; set; }
        public decimal Amount { get; set; }
    }
}