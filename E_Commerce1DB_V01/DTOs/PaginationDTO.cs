namespace E_Commerce1DB_V01.DTOs
{
    public class PaginationDTO<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)Count / PageSize);
        public List<T> Data { get; set; }
    }
}
