using Application.persistence.dtos;

namespace Application.dtos;

public class TestDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; } 
    public int Duration { get; set; }
    public bool IsPublic { get; set; }
    public string Status { get; set; } = null!;
    
    public List<CategoryDto> Categories { get; set; }
    public List<QuestionDto> Questions { get; set; }
    
}