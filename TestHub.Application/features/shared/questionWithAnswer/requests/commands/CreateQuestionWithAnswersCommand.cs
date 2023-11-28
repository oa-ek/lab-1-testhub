namespace Application.features.shared.questionwithanswer.requests.commands;

public class CreateQuestionWithAnswersCommand : IRequest<BaseCommandResponse<RespondQuestionDto>>
{
    public RequestQuestionWithAnswerDto? QuestionWithAnswerDto { get; set; }
    public required int TestId { get; set; }
}