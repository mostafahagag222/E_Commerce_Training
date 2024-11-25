namespace E_Commerce1DB_V01.DTOs
{
    public class BasketDTO
    {
            public string Id { get; set; }
            public decimal ShippingPrice { get; set; }
            public int DeliveryMethodId { get; set; }
            public List<ProductDTO> Items { get; set; }
    }
}
