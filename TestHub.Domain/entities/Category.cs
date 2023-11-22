using Domain.common;

namespace Domain.entities;

public class Category : BaseAuditableEntity
{
    public string Title { get; set; } = string.Empty;
    protected internal virtual ICollection<TestCategory> TestCategory { get; set; } = new List<TestCategory>();
}