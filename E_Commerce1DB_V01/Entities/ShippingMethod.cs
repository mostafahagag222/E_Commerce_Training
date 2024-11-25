namespace E_Commerce1DB_V01.Entities
{
    public class ShippingMethod
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string DeliveryTime { get; set; }

        //navigational
        public virtual List<Order> Orders { get; set; }

    }
}
