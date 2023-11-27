namespace Application.features.test.requests.commands;

public class DeleteTestCommand : IRequest<BaseCommandResponse<RespondTestDto>>
{
    public int Id { get; set; }
}