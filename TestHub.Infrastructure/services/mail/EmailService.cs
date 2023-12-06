using System.Net;
using System.Net.Mail;
using Application.contracts.infrastructure.email;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace TestHub.Infrastructure.services.mail;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> mailSettingsOptions)
    {
        _emailSettings = mailSettingsOptions.Value;
    }

    public async Task SendEmailAsync(EmailData data)
    {
        var fromEmail = _emailSettings.SenderEmail;
        var password = _emailSettings.Password;
        var username = _emailSettings.UserName;
        var emailUser = data.To;
        
        var mimeServer = new MimeMessage();

        mimeServer.From.Add(new MailboxAddress("TestHub", fromEmail));
        mimeServer.To.Add(MailboxAddress.Parse(emailUser));
                 
        mimeServer.Subject = "Confirm email TestHub ";
        mimeServer.Body = data.Body;
            
            
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(username, password);
        await smtp.SendAsync(mimeServer);
        await smtp.DisconnectAsync(true);
    }
}