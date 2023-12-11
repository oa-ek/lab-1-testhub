using Application.results.common;

namespace Application.features.question.requests.commands;

public class CreateQuestionCommand : IRequest<BaseCommandResult<RespondQuestionDto>>
{
    public RequestQuestionDto? QuestionDto { get; set; }
    public required int TestId { get; set; }
}