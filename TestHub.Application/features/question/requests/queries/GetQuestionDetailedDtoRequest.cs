using Application.results.common;

namespace Application.features.question.requests.queries;

public class GetQuestionDetailedDtoRequest : IRequest<BaseCommandResult<RespondQuestionDto>>
{
    public int Id { get; set; }
}