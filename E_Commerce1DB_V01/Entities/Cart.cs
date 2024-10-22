using E_Commerce1DB_V01.Repositories;

namespace E_Commerce1DB_V01
{
    public class Cart
    {
        public string Id { get; set; }
        public int TotalQuantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int? UserID { get; set; }
        public decimal TotalPrice { get; set; }
        public string ShippingMethodID { get; set; }

        //navigational
        public virtual User User { get; set; }
        public virtual ShippingMethod ShippingMethod { get; set; }
        public virtual List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public virtual Order Order { get; set; }
    }
}
