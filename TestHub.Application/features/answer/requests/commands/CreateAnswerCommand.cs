using Application.dtos.requestsDto;

namespace Application.features.answer.requests.commands;

public class CreateAnswerCommand : IRequest<BaseCommandResponse>
{
    public required RequestAnswerDto AnswerDto { get; set; }
}