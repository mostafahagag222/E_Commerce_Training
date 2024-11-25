namespace E_Commerce1DB_V01.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string OrderDate { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public string BuyerEmail { get; set; }
        public AddressDTO ShipAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal ShippingPrice { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
    }
}
