namespace Application.dtos.sharedDTOs.validators;

public class CategoryDtoValidator : AbstractValidator<CategoryDto>
{
    private const int MaxLength = 255;
    
    public CategoryDtoValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(MaxLength).WithMessage($"{{PropertyName}} must not exceed {MaxLength} characters.");
    }
}