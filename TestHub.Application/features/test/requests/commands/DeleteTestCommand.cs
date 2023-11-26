namespace Application.features.test.requests.commands;

public class DeleteTestCommand : IRequest<BaseCommandResponse>
{
    public int Id { get; set; }
}