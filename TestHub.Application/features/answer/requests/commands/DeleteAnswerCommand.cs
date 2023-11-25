namespace Application.features.answer.requests.commands;

public class DeleteAnswerCommand : IRequest<Unit>
{
    public int Id { get; set; }
}