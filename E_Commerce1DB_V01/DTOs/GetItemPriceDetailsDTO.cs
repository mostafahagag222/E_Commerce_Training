namespace E_Commerce1DB_V01.DTOs
{
    public class GetItemPriceDetailsDTO
    {
        public decimal ProductPrice { get; set; }
        public decimal BasketItemPrice { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
