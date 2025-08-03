using Application.Interfaces;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infrastructure.Email;

public class SendGridEmailService(IOptions<SendGridSettings> options) : IEmailService
{
    private readonly SendGridClient _client = new(options.Value.ApiKey);
    private readonly string _fromEmail = options.Value.FromEmail;

    public async Task SendAsync(string toEmail, string subject, string htmlContent)
    {
        var from = new EmailAddress(_fromEmail, "Root Quest");
        var to = new EmailAddress(toEmail);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
        await _client.SendEmailAsync(msg);
    }
}
