namespace E_Commerce1DB_V01.DTOs;

public class SendGridEmailDto
{
    public string RecipientEmail { get; set; }
    public string RecipientName { get; set; }
    public string Subject { get; set; }
    public string plainTextContent { get; set; }
    public string HtmlContent { get; set; }
    public string TemplateId { get; set; }
    public object Content { get; set; }
}