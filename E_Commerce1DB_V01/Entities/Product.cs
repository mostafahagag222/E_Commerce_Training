namespace E_Commerce1DB_V01.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int TypeId { get; set; }
        public int BrandId { get; set; }
        public int UnitsInStock {  get; set; }

        //navigational
        public virtual Type Type { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();

    }
}
