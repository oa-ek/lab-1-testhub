namespace TestHub.Core.Models;

public class Category
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<TestCategory> TestCategories { get; set; } = new List<TestCategory>();
}
