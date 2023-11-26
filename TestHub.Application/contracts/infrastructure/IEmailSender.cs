namespace Application.contracts.infrastructure;

public interface IEmailSender
{
    Task<bool> SendEmail(Email email);
}