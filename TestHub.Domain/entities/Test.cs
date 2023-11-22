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
        
        protected internal virtual Question Question { get; set; } = new Question();
        protected internal virtual ICollection<TestCategory> TestCategory { get; set; } = new List<TestCategory>();
        protected internal virtual ICollection<TestMetadata> TestMetadata { get; set; } = new List<TestMetadata>();
        protected internal virtual ICollection<TestSession> TestSession { get; set; } = new List<TestSession>();
    }
}