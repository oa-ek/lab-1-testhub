using Application.results.common;

namespace Application.features.shared.questionwithanswer.requests.commands;

public class CreateQuestionWithAnswersCommand : IRequest<BaseCommandResult<RespondQuestionDto>>
{
    public RequestQuestionWithAnswerDto? QuestionWithAnswerDto { get; set; }
    public required int TestId { get; set; }
}