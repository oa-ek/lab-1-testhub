namespace Application.features.question.requests.queries;

public class GetQuestionDetailedDtoRequest : IRequest<BaseCommandResponse<RespondQuestionDto>>
{
    public int Id { get; set; }
}