using System.ComponentModel.DataAnnotations;

namespace TestHub.Core.Dtos;

public class StatusQuestionSessionDto
{
    [Required] public int SessionId { get; set; }
    [Required] public int QuestionId { get; set; }
    public double? Point { get; set; }
    public bool? IsCorrect { get; set; }
}