using System.ComponentModel.DataAnnotations;

namespace TestHub.Core.Dtos;

public class TestSessionDto
{
    [Required] public int UserId { get; set; }
    [Required] public int TestId { get; set; }
    [Required] public bool IsTraining { get; set; }
    
}