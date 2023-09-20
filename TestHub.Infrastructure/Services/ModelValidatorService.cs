using System.ComponentModel.DataAnnotations;

namespace TestHub.Infrastructure.Services
{
    public class ModelValidatorService
    {
        public class ValidationResults
        {
            public bool IsValid { get; set; }
            public ICollection<ValidationResult>? Errors { get; set; }
        }

        public ValidationResults ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model, serviceProvider: null, items: null);

            bool isValid = Validator.TryValidateObject(model, context, validationResults, true);

            return new ValidationResults
            {
                IsValid = isValid,
                Errors = validationResults
            };
        }
    }
}