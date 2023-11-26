namespace Application.dtos.sharedDTOs.validators
{
    public class FileDtoValidator : AbstractValidator<FileDto>
    {
        private const int MaxLength = 255;
        
        public FileDtoValidator()
        {
            RuleFor(dto => dto.FileName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(MaxLength).WithMessage($"{{PropertyName}} cannot be longer than {MaxLength} characters.");

            RuleFor(dto => dto.ContentType)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(MaxLength).WithMessage($"{{PropertyName}} cannot be longer than {MaxLength} characters.");

            RuleFor(dto => dto.Data)
                .NotNull().WithMessage("{PropertyName} is required.")
                .NotEmpty().WithMessage("{PropertyName} cannot be empty.");
        }
    }
}