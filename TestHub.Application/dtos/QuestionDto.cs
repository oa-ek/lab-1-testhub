namespace Application.dtos;

public class QuestionDto
{
    public string Title { get; set; } 

    public string? Description { get; set; }
    
    public string Type { get; set; }

    public FileDto? Image { get; set; }
    
    public List<AnswerDto> Answers { get; set; }
}