namespace Application.dtos.requestsDto.validators;

public class RequestAnswerDtoValidator : AbstractValidator<RequestAnswerDto>
{
    private readonly IAnswerRepository _answerRepository;
    private const int MaxLength = 255;

    public RequestAnswerDtoValidator(IAnswerRepository answerRepository)
    {
        _answerRepository = answerRepository;

        RuleFor(p => p.Text)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(MaxLength).WithMessage($"{{PropertyName}} must not exceed {MaxLength} characters.");

        RuleFor(p => p.Image)
            .SetValidator(new FileDtoValidator()!)
            .When(p => p.Image != null);

        RuleFor(p => p.IsCorrect)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

        RuleFor(p => p.IsStrictText)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

        RuleFor(p => p.QuestionId)
            .GreaterThan(0)
            .MustAsync(ExistInRepository)
            .WithMessage("{PropertyName} does not exist.");
    }

    private async Task<bool> ExistInRepository(int id, CancellationToken token)
    {
        var questionExists = await _answerRepository.Exists(id);
        return questionExists;
    }
}