using Application.dtos.requestsDto;

namespace Application.features.question.requests.commands;

public class UpdateQuestionCommand : IRequest<BaseCommandResponse>
{
    public int Id { get; set; }
    public required RequestQuestionDto QuestionDto { get; set; }
}