namespace Application.features.answer.requests.commands;

public class DeleteAnswerCommand : IRequest<BaseCommandResponse>
{
    public int Id { get; set; }
}