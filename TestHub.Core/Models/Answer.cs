using System.ComponentModel.DataAnnotations;

namespace TestHub.Core.Models;

public class Answer
{
    public int Id { get; set; }

    [Required] [MaxLength(512)] public string Text { get; set; } = null!;

    [MaxLength(512)] public string Image { get; set; } = null!;

    [Required] public bool IsCorrect { get; set; }

    public int QuestionId { get; set; }

    [Required] public bool IsStrictText { get; set; }

    public Question Question { get; set; } = null!;
}

public class AnswerValidationResult
{
    public bool IsValid { get; set; }
    public ICollection<ValidationResult>? Errors { get; set; }
}

public static class AnswerValidator
{
    public static AnswerValidationResult ValidateTest(Test test)
    {
        var context = new ValidationContext(test, serviceProvider: null, items: null);
        var errors = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(test, context, errors, validateAllProperties: true);

        return new AnswerValidationResult
        {
            IsValid = isValid,
            Errors = errors
        };
    }
}

