namespace TestHub.Core.Models;

public class StatusSessionQuestion
{
    public int Id { get; set; }

    public int SessionId { get; set; }

    public int QuestionId { get; set; }

    public bool IsCorrect { get; set; }

    public int Attepts { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual TestSession Session { get; set; } = null!;
}
