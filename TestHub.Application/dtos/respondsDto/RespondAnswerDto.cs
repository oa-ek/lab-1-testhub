namespace Application.dtos.respondsDto;

public class RespondAnswerDto
{
    public required string Text { get; set; } 
    
    public string? Image { get; set; }
    
    public bool IsCorrect { get; set; }

    public bool IsStrictText { get; set; }
}