using System.ComponentModel.DataAnnotations;

namespace TestHub.Core.Interfaces;

public class TestValidationResult
{
    public bool IsValid { get; set; }
    public ICollection<ValidationResult> Errors { get; set; }
}