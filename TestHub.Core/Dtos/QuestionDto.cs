using System.ComponentModel.DataAnnotations;

namespace TestHub.Core.Dtos;

public class QuestionDto
{
    [Required] [MaxLength(215)] public string Title { get; set; } = null!;

    [Required] [MaxLength(512)] public string? Description { get; set; }

    [MaxLength(512)] public string? Image { get; set; }
}