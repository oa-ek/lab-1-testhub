namespace TestHub.Core.Models;

public class RefreshToken
{
    public required string Token { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime Expires { get; set; }
}