namespace E_Commerce1DB_V01.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //navigational
        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}
