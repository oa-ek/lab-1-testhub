using Application.dtos;
using Application.dtos.requestsDto;

namespace Application.features.answer.requests.queries;

public class GetAnswerDtoRequest : IRequest<RequestAnswerDto>
{
    public int Id { get; set; }
}