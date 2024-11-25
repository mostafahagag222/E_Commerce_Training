namespace E_Commerce1DB_V01.Payloads
{
    public class GetProductsPagePayload
    {
        public int? BrandID { get; set; }
        public int? TypeID { get; set; }
        public string Sort { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; }
    }
}
