namespace TestHub.Core.Models;

public class QuestionType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
