using Domain.common;

namespace Domain.entities;

public class Category : BaseAuditableEntity
{
    public string Title { get; set; } = string.Empty;
    public virtual ICollection<TestCategory> TestCategories { get; set; } = new List<TestCategory>();
}