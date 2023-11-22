using Domain.common;

namespace Domain.entities;

public class Answer : BaseAuditableEntity
{
    public string Text { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public bool IsCorrect { get; set; }
    public bool IsStrictText { get; set; }

    public int QuestionId { get; set; }
    protected internal virtual Question Question { get; set; } = new Question();
}