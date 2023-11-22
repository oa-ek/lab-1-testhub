using Domain.common;

namespace Domain.entities;

public class TestMetadata : BaseAuditableEntity
{
    public byte Like { get; set; }
    public int Rating { get; set; }

    public int TestId { get; set; }
    public virtual Test Test { get; set; } = new Test()!;

    public int UserId { get; set; }
    public virtual User User { get; set; } = new User()!;
}