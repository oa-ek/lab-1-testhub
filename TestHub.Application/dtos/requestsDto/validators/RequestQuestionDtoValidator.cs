using Application.dtos.sharedDTOs.validators;
using Application.persistence.contracts;

namespace Application.dtos.requestsDto.validators
{
    public class RequestQuestionDtoValidator : AbstractValidator<RequestQuestionDto>
    {
        private readonly IQuestionRepository _questionRepository;
        private const int MaxLength = 255;

        public RequestQuestionDtoValidator(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;

            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(MaxLength).WithMessage($"{{PropertyName}} must not exceed {MaxLength} characters.");

            RuleFor(p => p.Type)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Image)
                .SetValidator(new FileDtoValidator()!)
                .When(p => p.Image != null);

            RuleFor(p => p.TestId)
                .GreaterThan(0)
                .MustAsync(ExistInRepository)
                .WithMessage("{PropertyName} does not exist.");
        }

        private async Task<bool> ExistInRepository(int id, CancellationToken token)
        {
            var questionExists = await _questionRepository.Exists(id);
            return questionExists;
        }
    }
}