namespace Application.features.shared.questionwithanswer.requests.commands;

public class DeleteQuestionWithAnswersCommand : IRequest<BaseCommandResponse<RespondQuestionDto>>
{
    public required int QuestionId { get; set; }
}