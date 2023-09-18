using System.ComponentModel.DataAnnotations;
using TestHub.Core.Interfaces;
using TestHub.Core.Models;

namespace TestHub.Core.Validators;

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