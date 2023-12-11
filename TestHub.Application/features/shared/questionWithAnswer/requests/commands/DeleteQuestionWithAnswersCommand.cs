using Application.results.common;

namespace Application.features.shared.questionwithanswer.requests.commands;

public class DeleteQuestionWithAnswersCommand : IRequest<BaseCommandResult<RespondQuestionDto>>
{
    public required int QuestionId { get; set; }
}