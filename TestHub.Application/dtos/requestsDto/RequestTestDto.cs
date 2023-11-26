namespace Application.dtos.requestsDto;

public class RequestTestDto
{
    public required string Title { get; set; } 
    public string? Description { get; set; } 
    public int Duration { get; set; }
    public bool IsPublic { get; set; }
    public string Status { get; set; } = null!;
}