using E_Commerce1DB_V01.Entities;

namespace E_Commerce1DB_V01.DTOs
{
    public class CreateOrderItemDTO
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public Product Product { get; set; }

    }
}
