namespace E_Commerce1DB_V01.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string BasketId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int TotalQuantity { get; set; }
        public Decimal TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.PendingDelivery;
        public string ShippingMethodId { get; set; }
        public decimal SubTotal { get; set; }

        //navigational
        public virtual User User { get; set; } 
        public virtual Basket Basket { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual List<Payment> Payments { get; set; } = new List<Payment>();
        public virtual ShippingMethod ShippingMethod { get; set; }



    }
}
