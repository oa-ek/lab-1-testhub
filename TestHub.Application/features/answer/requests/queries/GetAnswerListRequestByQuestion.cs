namespace Application.features.answer.requests.queries;

public class GetAnswerListRequestByQuestion : IRequest<List<Answer>>
{
    public int QuestionId { get; set; }
}
