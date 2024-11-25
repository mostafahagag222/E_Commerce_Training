using Newtonsoft.Json;

namespace E_Commerce1DB_V01.DTOs;

public class SendGridEventDto
{
    [JsonProperty("attempt")]
    public string Attempt { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("event")]
    public string Event { get; set; }

    [JsonProperty("response")]
    public string Response { get; set; }

    [JsonProperty("sg_event_id")]
    public string SgEventId { get; set; }

    [JsonProperty("sg_message_id")]
    public string SgMessageId { get; set; }

    [JsonProperty("smtp-id")]
    public string SmtpId { get; set; }

    [JsonProperty("timestamp")]
    public long Timestamp { get; set; }

    [JsonProperty("tls")]
    public bool Tls { get; set; }
}