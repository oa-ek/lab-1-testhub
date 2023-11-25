using Domain.common;

namespace Domain.entities;

public class QuestionType : BaseAuditableEntity
{
    public string Type { get; set; } = string.Empty;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}