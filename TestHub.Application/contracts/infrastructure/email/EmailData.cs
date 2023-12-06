using MimeKit;

namespace Application.contracts.infrastructure.email;

public class EmailData
{
    public string To { get; init; } = string.Empty;
    public string ToName { get; init; } = string.Empty;
    public string Subject { get; init; }  = string.Empty;
    public MimeEntity Body { get; init; } = null!;
}