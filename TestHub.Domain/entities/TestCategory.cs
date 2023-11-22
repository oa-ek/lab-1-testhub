using Domain.common;

namespace Domain.entities;

public class TestCategory : BaseAuditableEntity
{
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; } = new Category();

    public int TestId { get; set; }
    public virtual Test Test { get; set; } = new Test();
}