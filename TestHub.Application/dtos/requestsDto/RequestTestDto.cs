namespace Application.dtos.requestsDto;

public class RequestTestDto
{
    public required string Title { get; set; } 
    public string? Description { get; set; } 
    public int Duration { get; set; }
    public string Status { get; set; } = null!;
    public bool IsPublic { get; set; }
    
    public CategoryDto[] Categories { get; set; } = null!;
}