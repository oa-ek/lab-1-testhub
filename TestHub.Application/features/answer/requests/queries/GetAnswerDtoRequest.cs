using Application.dtos;

namespace Application.features.answer.requests.queries;

public class GetAnswerDtoRequest : IRequest<AnswerDto>
{
    public int Id { get; set; }
}