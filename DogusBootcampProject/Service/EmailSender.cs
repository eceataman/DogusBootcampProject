using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _config;

    public EmailSender(IConfiguration config) => _config = config;

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var smtp = _config.GetSection("Smtp");
        var client = new SmtpClient(smtp["Host"], int.Parse(smtp["Port"]))
        {
            Credentials = new NetworkCredential(smtp["Username"], smtp["Password"]),
            EnableSsl = true
        };

        var mail = new MailMessage(smtp["Username"], email, subject, htmlMessage)
        {
            IsBodyHtml = true
        };

        return client.SendMailAsync(mail);
    }
}
