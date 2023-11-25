using Application.dtos.requestsDto;

namespace Application.features.question.requests.commands;

public class CreateQuestionCommand : IRequest<int>
{
    public RequestQuestionDto QuestionDto { get; set; }
}