
namespace Application.common.models;

public class TestDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Duration { get; set; }
    public string[] Categories { get; set; } = Array.Empty<string>();
    public bool IsPublic { get; set; }
    public string Status { get; set; } = string.Empty;
}