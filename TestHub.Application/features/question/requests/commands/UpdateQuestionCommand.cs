using Application.dtos.requestsDto;

namespace Application.features.question.requests.commands;

public class UpdateQuestionCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public required RequestQuestionDto QuestionDto { get; set; }
}