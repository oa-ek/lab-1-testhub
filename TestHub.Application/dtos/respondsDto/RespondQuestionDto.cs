namespace Application.dtos.respondsDto;

public class RespondQuestionDto
{
    public required string Title { get; set; } 
    public string? Description { get; set; }
    public required string Type { get; set; }
    public string? ImageUrl { get; set; }
    
    public List<RespondAnswerDto>? Answers { get; set; }
}