using Application.dtos;
using Application.dtos.requestsDto;

namespace Application.features.question.requests.queries;

public class GetQuestionDtoRequest: IRequest<RequestQuestionDto>
{
    public int Id { get; set; }
}