using Domain.common;

namespace Domain.entities
{
    public class User : BaseAuditableEntity
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? Comment { get; set; }

        public DateTime? Deleted { get; set; }

        public string Token { get; set; } = string.Empty;
        public bool IsEmailVerified { get; set; }
        public DateTimeOffset? TokenCreated { get; set; }
        public DateTimeOffset? TokenExpires { get; set; }

        public string? PasswordResetToken { get; set; }
        public DateTimeOffset? ResetTokenExpires { get; set; }


        public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
        public virtual ICollection<TestSession> TestSessions { get; set; } = new List<TestSession>();
        public virtual ICollection<TestMetadata> TestMetadata { get; set; } = new List<TestMetadata>();
    }
}