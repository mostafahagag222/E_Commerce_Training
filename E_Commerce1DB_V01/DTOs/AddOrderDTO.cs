namespace E_Commerce1DB_V01.DTOs
{
    public class AddOrderDTO
    {
        public List<GetItemPriceDetailsDTO> Items { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}
