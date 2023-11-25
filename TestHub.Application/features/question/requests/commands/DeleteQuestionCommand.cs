namespace Application.features.question.requests.commands;

public class DeleteQuestionCommand : IRequest<Unit>
{
    public int Id { get; set; }
}