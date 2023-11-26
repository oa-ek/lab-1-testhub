namespace Application.dtos.requestsDto.validators
{
    public class RequestTestDtoValidator : AbstractValidator<RequestTestDto>
    {
        private const int MaxDurationRate = 255;
        
        public RequestTestDtoValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Duration)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
                .LessThan(MaxDurationRate).WithMessage($"{{PropertyName}} must be less than {MaxDurationRate}.");

            RuleFor(p => p.Status)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.IsPublic)
                .NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Description)
                .MaximumLength(255).WithMessage("{PropertyName} must not exceed 255 characters.");
        }
    }
}