namespace Application.features.test.requests.queries;

public class GetTestDetailedDtoRequest: IRequest<BaseCommandResponse<RespondTestDto>>
{
    public int Id { get; set; }
}