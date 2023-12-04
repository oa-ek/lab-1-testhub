namespace Application.contracts.persistence.authentication;

public interface IDateTimeProvider
{
    static DateTime UtcNow { get; }
}