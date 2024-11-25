using Newtonsoft.Json;

namespace E_Commerce1DB_V01.DTOs
{
    public class UpaymentsWebHookResponseDTO
    {
        [JsonProperty("payment_id")]
        public string PaymentId { get; set; }
        [JsonProperty("result")]
        public string Result { get; set; }
        [JsonProperty("post_date")]
        public string PostDate { get; set; }
        [JsonProperty("tran_id")]
        public string TranId { get; set; }
        [JsonProperty("ref")]
        public string Ref { get; set; }
        [JsonProperty("track_id")]
        public string TrackId { get; set; }
        [JsonProperty("auth")]
        public string Auth { get; set; }
        [JsonProperty("order_id")]
        public string OrderId { get; set; }
        [JsonProperty("requested_order_id")]
        public string GUID { get; set; }
        [JsonProperty("refund_order_id")]
        public string RefundOrderId { get; set; }
        [JsonProperty("payment_type")]
        public string PaymentType { get; set; }
        [JsonProperty("invoice_id")]
        public string InvoiceId { get; set; }
        [JsonProperty("transaction_date")]
        public string TransactionDate { get; set; }
        [JsonProperty("receipt_id")]
        public string ReceiptId { get; set; }
        [JsonProperty("trn_udf")]
        public string TrnUDF { get; set; }
    }
}
