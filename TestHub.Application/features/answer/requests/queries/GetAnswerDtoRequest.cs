using Application.results.common;

namespace Application.features.answer.requests.queries;

public class GetAnswerDtoRequest : IRequest<BaseCommandResult<RespondAnswerDto>> 
{
    public int Id { get; set; }
}