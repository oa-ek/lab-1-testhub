namespace Application.features.question.requests.commands;

public class DeleteQuestionCommand : IRequest<BaseCommandResponse<RespondQuestionDto>>
{
    public int Id { get; set; }
}