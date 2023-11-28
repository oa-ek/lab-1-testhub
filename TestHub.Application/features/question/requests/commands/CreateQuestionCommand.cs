namespace Application.features.question.requests.commands;

public class CreateQuestionCommand : IRequest<BaseCommandResponse<RespondQuestionDto>>
{
    public required RequestQuestionDto? QuestionDto { get; set; }
    public required int TestId { get; set; }
}