namespace TestHub.Infrastructure.services.authentication;

public class DateTimeProvider : IDateTimeProvider
{
    public static DateTime UtcNow => DateTime.UtcNow;
}