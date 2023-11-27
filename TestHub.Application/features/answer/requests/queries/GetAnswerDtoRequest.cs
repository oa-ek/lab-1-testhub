namespace Application.features.answer.requests.queries;

public class GetAnswerDtoRequest : IRequest<BaseCommandResponse<RespondAnswerDto>> 
{
    public int Id { get; set; }
}