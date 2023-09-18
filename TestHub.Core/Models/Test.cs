namespace TestHub.Core.Models;

using System.ComponentModel.DataAnnotations;

public enum TestStatus
{
    Draft,
    Active,
    Archived
}

public class Test
{
    [Required] public int Id { get; set; }

    [Required] [MaxLength(255)] public string Title { get; set; } = null!;

    [MaxLength(512)] public string? Description { get; set; }

    [Required] public int Duration { get; set; }

    [Required] public int OwnerId { get; set; }

    [Required]
    [EnumDataType(typeof(TestStatus))]
    public TestStatus Status { get; set; } = TestStatus.Draft;

    [Required] public bool IsPublic { get; set; }

    [Required] public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    [Required] public virtual User Owner { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<TestCategory> TestCategories { get; set; } = new List<TestCategory>();

    public virtual ICollection<TestMetadata> TestMetadata { get; set; } = new List<TestMetadata>();

    public virtual ICollection<TestSession> TestSessions { get; set; } = new List<TestSession>();
}

public class TestValidationResult
{
    public bool IsValid { get; set; }
    public ICollection<ValidationResult>? Errors { get; set; }
}

public static class TestValidator
{
    public static TestValidationResult ValidateTest(Test test)
    {
        var context = new ValidationContext(test, serviceProvider: null, items: null);
        var errors = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(test, context, errors, validateAllProperties: true);

        return new TestValidationResult
        {
            IsValid = isValid,
            Errors = errors
        };
    }
}