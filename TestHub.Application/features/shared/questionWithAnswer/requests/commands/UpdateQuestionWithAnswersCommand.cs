namespace Application.features.shared.questionwithanswer.requests.commands;

public class UpdateQuestionWithAnswersCommand : IRequest<BaseCommandResponse<RespondQuestionDto>>
{
    public RequestQuestionWithAnswerDto? QuestionWithAnswerDto { get; set; }
    public required int QuestionId { get; set; }
}