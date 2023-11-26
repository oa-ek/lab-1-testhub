using FluentValidation.Results;

namespace Application.exceptions
{
    public class ValidationException : ApplicationException
    {
        public static List<string>? Errors { get; set; }

        public ValidationException(ValidationResult result)
        {
            Errors = result.Errors.Select(error => error.ErrorMessage).ToList();
        }
    }
}