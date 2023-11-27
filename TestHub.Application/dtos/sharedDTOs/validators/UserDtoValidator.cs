namespace Application.dtos.sharedDTOs.validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        private const int MinLength = 6;
        private const int MaxLength = 255;

        public UserDtoValidator()
        {
            RuleFor(userDto => userDto.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(MaxLength).WithMessage($"Email must not exceed {MaxLength} characters.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(userDto => userDto.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(MinLength).WithMessage("Password must be at least 6 characters long.")
                .Must(ContainDigitAndSpecialCharacter)
                .WithMessage("Password must contain at least one digit and one special character.");
            
            RuleFor(userDto => userDto.Name)
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