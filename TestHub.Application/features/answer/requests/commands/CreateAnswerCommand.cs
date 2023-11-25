using Application.dtos.requestsDto;

namespace Application.features.answer.requests.commands;

public class CreateAnswerCommand : IRequest<int>
{
    public RequestAnswerDto AnswerDto { get; set; }
}