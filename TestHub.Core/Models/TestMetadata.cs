namespace TestHub.Core.Models;

public class TestMetadata
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int TestId { get; set; }

    public byte Like { get; set; }

    public int Rating { get; set; }

    public virtual Test Test { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
