using System.ComponentModel.DataAnnotations;

namespace TestHub.Core.Dtos;

public class TestDto
{
    [Required] [MaxLength(255)] public string Title { get; set; } = null!;
    
    [Required] [MaxLength(512)] public string? Description { get; set; }
    
    [Required] [MinLength(1)] [MaxLength(1440)] public int Duration { get; set; }
    
    [Required] public bool IsPublic { get; set; }
    
}