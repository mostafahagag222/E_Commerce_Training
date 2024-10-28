using E_Commerce1DB_V01.DTOs;

namespace E_Commerce2Business_V01
{
    public class CreateRequestBodyDTO
    {
        public PaymentRequestDTO RequestBody { get; set; }
        public decimal PaymentAmount { get; set; }
        public string GUID { get; set; }
    }
}