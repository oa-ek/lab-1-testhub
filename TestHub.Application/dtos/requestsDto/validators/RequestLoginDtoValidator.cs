namespace Application.dtos.requestsDto.validators;

public class RequestLoginDtoValidator : AbstractValidator<RequestLoginDto>
{
    private const int MaxLength = 255;

    public RequestLoginDtoValidator()
    {
        RuleFor(registerDto => registerDto.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(MaxLength).WithMessage($"Email must not exceed {MaxLength} characters.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}