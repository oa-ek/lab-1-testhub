namespace Application.dtos.requestsDto;

public class RequestAnswerDto
{
    public required string Text { get; set; } 
    
    public FileDto? Image { get; set; }
    
    public bool IsCorrect { get; set; }

    public bool IsStrictText { get; set; }
    public int QuestionId { get; set; }
}