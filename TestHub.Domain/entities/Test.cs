using Domain.common;

namespace Domain.entities
{
    public class Test : BaseAuditableEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool IsPublic { get; set; }
        
        public int OwnerId { get; set; }
        public virtual User Owner { get; set; } = new User();
        
        public virtual Question Question { get; set; } = new Question();
        public virtual ICollection<TestCategory> TestCategories { get; set; } = new List<TestCategory>();
        public virtual ICollection<TestMetadata> TestMetadata { get; set; } = new List<TestMetadata>();
        public virtual ICollection<TestSession> TestSessions { get; set; } = new List<TestSession>();
        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}