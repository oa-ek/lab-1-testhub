using Application.dtos.requestsDto;
using Application.persistence.dtos;

namespace Application.dtos.respondsDto;

public class RespondTestDto
{
    public required string Title { get; set; } 
    public string? Description { get; set; } 
    public int Duration { get; set; }
    public bool IsPublic { get; set; }
    public string Status { get; set; } = null!;
    
    public List<CategoryDto>? Categories { get; set; }
    public List<RequestQuestionDto>? Questions { get; set; }
    
}