namespace Application.dtos;

public class AnswerDto
{
    public string Text { get; set; } 
    
    public FileDto? Image { get; set; }
    
    public bool IsCorrect { get; set; }

    public bool IsStrictText { get; set; }
}