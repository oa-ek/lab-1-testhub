namespace TestHub.Core.Models;

public class Question
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Image { get; set; }

    public int TestId { get; set; }

    public int TypeId { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual ICollection<StatusSessionQuestion> StatusSessionQuestions { get; set; } = new List<StatusSessionQuestion>();

    public virtual Test Test { get; set; } = null!;

    public virtual QuestionType Type { get; set; } = null!;
}
