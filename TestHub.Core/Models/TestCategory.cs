namespace TestHub.Core.Models;

public class TestCategory
{
    public int Id { get; set; }

    public int TestId { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Test Test { get; set; } = null!;
}
