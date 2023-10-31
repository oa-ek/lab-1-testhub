namespace TestHub.Core.Models;

public class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public DateTime? DeleteAt { get; set; }

    public string? Comment { get; set; }


    public string? Token { get; set; } = string.Empty;
    
    public bool IsVerifiedEmail { get; set; } = false;
    public DateTime? TokenCreated { get; set; }
    public DateTime? TokenExpires { get; set; }

    public string? PasswordResetToken { get; set; }
    
    public DateTime? ResetTokenExpires { get; set; }
    
    public virtual ICollection<TestMetadata> TestMetadata { get; set; } = new List<TestMetadata>();

    public virtual ICollection<TestSession> TestSessions { get; set; } = new List<TestSession>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
