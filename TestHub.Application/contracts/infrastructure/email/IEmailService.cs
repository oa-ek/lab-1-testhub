namespace Application.contracts.infrastructure.email;

public interface IEmailService
{
    Task SendEmailAsync(EmailData data);
}
