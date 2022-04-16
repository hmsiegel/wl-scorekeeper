using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

using SendGrid;
using SendGrid.Helpers.Mail;

namespace CompDirectorAPI.Library.Services;

public class EmailSender : IEmailSender
{
    public AuthMessageSenderOptions Options { get; }

    public EmailSender(IOptions<AuthMessageSenderOptions> options)
    {
        Options = options.Value;   
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        if (string.IsNullOrEmpty(Options.SendGridKey))
        {
            throw new Exception("Null SendGridKey");
        }

        await Execute(Options.SendGridKey, subject, message, email);
    }

    private async Task Execute(string apiKey, string subject, string message, string toEmail)
    {
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress("hmsiegel@gmail.com", "Password Recovery"),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        msg.AddTo(new EmailAddress(toEmail));

        msg.SetClickTracking(false, false);
        var response = await client.SendEmailAsync(msg);
    }
}
