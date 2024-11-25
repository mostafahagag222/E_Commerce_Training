using E_Commerce1DB_V01.DTOs;
using E_Commerce2Business_V01.Integrations;
using MassTransit;

public class SendEmailConsumer : IConsumer<SendGridEmailDto>
{
    private readonly SendGridSendEmailService _sendGridSendEmailService;

    public SendEmailConsumer(SendGridSendEmailService sendGridSendEmailService)
    {
        _sendGridSendEmailService = sendGridSendEmailService;
    }

    public async Task Consume(ConsumeContext<SendGridEmailDto> context)
    {
        await _sendGridSendEmailService.SendTemplateEmailAsync(context.Message);
    }
}