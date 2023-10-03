using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TestHub.Infrastructure.Services.Email
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string subject, string body, string to)
        {
            if (string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(body) || string.IsNullOrEmpty(to))
            {
                _logger.LogError("Invalid email parameters.");
                return;
            }

            try
            {
                var smtpClient = new SmtpClient
                {
                    Host = _configuration["Smtp:Host"],
                    Port = int.TryParse(_configuration["Smtp:Port"], out var port) ? port : 25,
                    EnableSsl = bool.TryParse(_configuration["Smtp:EnableSsl"], out var enableSsl) && enableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(
                        _configuration["Smtp:Username"],
                        _configuration["Smtp:Password"])
                };

                using (var message = new MailMessage())
                {
                    message.To.Add(to);
                    message.From = new MailAddress(_configuration["Smtp:FromEmail"]);
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true; // Дозволяє використовувати HTML у повідомленні

                    await smtpClient.SendMailAsync(message);
                    _logger.LogInformation("Email '{0}' to '{1}' was sent successfully.", subject, to);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email.");
            }
        }
    }
}
