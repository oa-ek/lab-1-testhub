namespace TestHub.Core.Dtos;

public class AnswerDto
{
    public string Text { get; set; } = null!;
    
    public FileDto? a_image { get; set; }
    
    public bool IsCorrect { get; set; }

    public bool IsStrictText { get; set; }
}
