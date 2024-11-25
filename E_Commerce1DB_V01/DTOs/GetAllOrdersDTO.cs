namespace E_Commerce1DB_V01.DTOs
{
    public class GetAllOrdersDTO
    {
        public int Id { get; set; }
        public string OrderDate { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
    }
}
