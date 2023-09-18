using TestHub.Core.Enum;

namespace TestHub.Core.Models;

using System.ComponentModel.DataAnnotations;

public class Test
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int Duration { get; set; }

    public int OwnerId { get; set; }
    
    public TestStatus Status { get; set; }

    public bool IsPublic { get; set; }

    [Required] public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [Required] public virtual User Owner { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<TestCategory> TestCategories { get; set; } = new List<TestCategory>();

    public virtual ICollection<TestMetadata> TestMetadata { get; set; } = new List<TestMetadata>();

    public virtual ICollection<TestSession> TestSessions { get; set; } = new List<TestSession>();
}