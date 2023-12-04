namespace Application.dtos.requestsDto.validators
{
    public class RequestRegisterDtoValidator : AbstractValidator<RequestRegisterDto>
    {
        private const int MinLength = 6;
        private const int MaxLength = 255;

        public RequestRegisterDtoValidator()
        {
            RuleFor(registerDto => registerDto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(MaxLength).WithMessage($"Email must not exceed {MaxLength} characters.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(registerDto => registerDto.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(MinLength).WithMessage("Password must be at least 6 characters long.")
                .Must(ContainDigitAndSpecialCharacter)
                .WithMessage("Password must contain at least one digit and one special character.");
            
            RuleFor(registerDto => registerDto.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(MaxLength).WithMessage($"Name must not exceed {MaxLength} characters.");
        }
        
        private static bool ContainDigitAndSpecialCharacter(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && 
                   password.Any(char.IsDigit) &&
                   password.Any(ch => !char.IsLetterOrDigit(ch));
        }
    }
}