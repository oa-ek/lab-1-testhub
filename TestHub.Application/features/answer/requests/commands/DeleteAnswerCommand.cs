namespace Application.features.answer.requests.commands;

public class DeleteAnswerCommand : IRequest<BaseCommandResponse<RespondAnswerDto>>
{
    public int Id { get; set; }
}