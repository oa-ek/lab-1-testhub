namespace TestHub.Core.Dtos;

public class QuestionDto
{
    public string Title { get; set; } = null!;

    public string? Description { get; set; }
    
    public string Type { get; set; }

    public FileDto? q_image { get; set; }
    
    public AnswerDto[] Answers { get; set; }
}