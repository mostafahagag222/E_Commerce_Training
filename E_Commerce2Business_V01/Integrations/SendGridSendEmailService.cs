using E_Commerce1DB_V01.DTOs;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace E_Commerce2Business_V01.Integrations;

public class SendGridSendEmailService
{
    private readonly IConfiguration _configuration;

    public SendGridSendEmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private async Task SendEmailUsingSendGridAsync(SendGridEmailDto emailDto)
    {
        var apiKey = _configuration["SendGrid:ApiKey"];
        var client = new SendGridClient(apiKey);
        var senderName = _configuration["SendGrid:SenderName"];
        var SenderEmail = _configuration["SendGrid:SenderEmail"];
        var from = new EmailAddress(SenderEmail, senderName);
        var subject = emailDto.Subject;
        var to = new EmailAddress(emailDto.RecipientEmail, emailDto.RecipientName);
        var plainTextContent = emailDto.plainTextContent;
        var htmlContent = emailDto.HtmlContent;
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        await client.SendEmailAsync(msg);
    }

    public async Task SendTemplateEmailAsync(SendGridEmailDto emailDto)
    {
        var apiKey = _configuration["SendGrid:ApiKey"];
        var client = new SendGridClient(apiKey);
        var senderName = _configuration["SendGrid:SenderName"];
        var SenderEmail = _configuration["SendGrid:SenderEmail"];
        var from = new EmailAddress(SenderEmail, senderName);
        var to = new EmailAddress(emailDto.RecipientEmail, emailDto.RecipientName);

        var msg = new SendGridMessage
        {
            From = from,
            TemplateId = emailDto.TemplateId
        };

        // Set dynamic template data
        msg.AddTo(to);
        msg.SetTemplateData(emailDto.Content);
        await client.SendEmailAsync(msg);
    }
}