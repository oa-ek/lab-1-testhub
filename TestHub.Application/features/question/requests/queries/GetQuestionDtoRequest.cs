using Application.dtos;

namespace Application.features.question.requests.queries;

public class GetQuestionDtoRequest: IRequest<QuestionDto>
{
    public int Id { get; set; }
}