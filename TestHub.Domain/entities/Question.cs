using Domain.common;

namespace Domain.entities;

public class Question : BaseAuditableEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Image { get; set; }
   
    public int TestId { get; set; }

    public int TypeId { get; set; } 
    public virtual QuestionType Type { get; set; } = new QuestionType();
    
    protected internal virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
    protected internal virtual ICollection<StatusSessionQuestion> StatusSessionQuestions { get; set; } = new List<StatusSessionQuestion>();
}