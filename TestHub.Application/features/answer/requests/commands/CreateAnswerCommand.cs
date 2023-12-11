using Application.results.common;

namespace Application.features.answer.requests.commands;

public class CreateAnswerCommand : IRequest<BaseCommandResult<RespondAnswerDto>>
{
    public required RequestAnswerDto? AnswerDto { get; set; }
    public int QuestionId { get; set; }
}