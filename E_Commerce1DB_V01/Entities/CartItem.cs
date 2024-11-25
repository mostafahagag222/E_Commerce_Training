namespace E_Commerce1DB_V01.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string BasketID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Price * Quantity;

        //navigational
        public virtual Basket Basket { get; set; }
        public virtual Product Product { get; set; }
    }
}
