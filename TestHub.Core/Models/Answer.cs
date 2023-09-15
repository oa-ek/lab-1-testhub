namespace TestHub.Core.Models;

public class Answer
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public string Image { get; set; } = null!;

    public bool IsCorrect { get; set; }

    public int QuestionId { get; set; }

    public bool IsStrictText { get; set; }

    public Question Question { get; set; } = null!;
}
