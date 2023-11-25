using Application.dtos.respondsDto;

namespace Application.features.question.requests.queries;

public class GetQuestionDtoRequest: IRequest<RespondQuestionDto>
{
    public int Id { get; set; }
}