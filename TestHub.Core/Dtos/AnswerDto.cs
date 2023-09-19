using System.ComponentModel.DataAnnotations;

namespace TestHub.Core.Dtos;

public class AnswerDto
{
    [Required] [MaxLength(512)] public string Text { get; set; } = null!;
    
    public string? Image { get; set; }
    
    [Required] public bool IsCorrect { get; set; }

    [Required] public bool IsStrictText { get; set; }
}