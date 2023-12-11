using Application.results.common;

namespace Application.features.shared.questionwithanswer.requests.commands;

public class UpdateQuestionWithAnswersCommand : IRequest<BaseCommandResult<RespondQuestionDto>>
{
    public RequestQuestionWithAnswerDto? QuestionWithAnswerDto { get; set; }
    public required int QuestionId { get; set; }
}