namespace Application.features.question.requests.commands;

public class DeleteQuestionCommand : IRequest<BaseCommandResponse>
{
    public int Id { get; set; }
}