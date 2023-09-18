namespace TestHub.Core.Models;

public class TestSession
{
    public int Id { get; set; }

    public DateTime StartedAt { get; set; }

    public DateTime FinishedAt { get; set; }

    public int UserId { get; set; }

    public int TestId { get; set; }

    public int? Result { get; set; }

    public int IsTraining { get; set; }

    public virtual ICollection<StatusSessionQuestion> StatusSessionQuestions { get; set; } = new List<StatusSessionQuestion>();

    public virtual Test Test { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
