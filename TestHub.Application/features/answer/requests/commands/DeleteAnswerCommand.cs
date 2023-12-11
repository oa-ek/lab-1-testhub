using Application.results.common;

namespace Application.features.answer.requests.commands;

public class DeleteAnswerCommand : IRequest<BaseCommandResult<RespondAnswerDto>>
{
    public int Id { get; set; }
}