namespace Application.features.test.requests.commands;

public class DeleteTestCommand : IRequest<Unit>
{
    public int Id { get; set; }
}