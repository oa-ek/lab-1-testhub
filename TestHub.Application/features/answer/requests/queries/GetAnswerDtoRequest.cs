using Application.dtos.respondsDto;

namespace Application.features.answer.requests.queries;

public class GetAnswerDtoRequest : IRequest<RespondAnswerDto>
{
    public int Id { get; set; }
}