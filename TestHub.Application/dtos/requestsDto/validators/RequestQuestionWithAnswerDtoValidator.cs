namespace Application.dtos.requestsDto.validators;

public class RequestQuestionWithAnswerDtoValidator : AbstractValidator<RequestQuestionWithAnswerDto>
{
    public RequestQuestionWithAnswerDtoValidator()
    {
        RuleFor(p => p.Answers)
            .NotEmpty().WithMessage("{PropertyName} is required.");

    }
}