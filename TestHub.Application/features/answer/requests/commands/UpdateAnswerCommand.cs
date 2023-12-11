using Application.results.common;

namespace Application.features.answer.requests.commands;

public class UpdateAnswerCommand: IRequest<BaseCommandResult<RespondAnswerDto>>
{
    public int Id { get; set; }
    public required RequestAnswerDto? AnswerDto { get; set; }
}