namespace Application.dtos.requestsDto;

public class RequestQuestionDto
{
    public required string Title { get; set; } 
    public string? Description { get; set; }
    public required string Type { get; set; }
    public FileDto? Image { get; set; }
    public int TestId { get; set; }
}