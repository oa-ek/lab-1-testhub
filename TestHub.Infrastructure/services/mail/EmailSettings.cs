namespace TestHub.Infrastructure.services.mail;

public class EmailSettings
{
    public const string SectionName = "MailSettings";
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string EnableSsl { get; set; } = string.Empty;
    public string SenderEmail { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}