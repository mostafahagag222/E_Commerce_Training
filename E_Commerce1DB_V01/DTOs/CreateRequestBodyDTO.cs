namespace E_Commerce1DB_V01.DTOs
{
    public class CreateRequestBodyDTO
    {
        public PaymentRequestDTO RequestBody { get; set; }
        public decimal PaymentAmount { get; set; }
        public string GUID { get; set; }
    }
}