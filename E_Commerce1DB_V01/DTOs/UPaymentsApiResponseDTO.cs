namespace E_Commerce1DB_V01.DTOs
{
    public class UPaymentsApiResponseDTO
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public ResponseData Data { get; set; }
    }

    public class ResponseData
    {
        public string Link { get; set; }
    }
}
