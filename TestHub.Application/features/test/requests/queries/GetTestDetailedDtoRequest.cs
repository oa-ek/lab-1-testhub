using Application.dtos.requestsDto;

namespace Application.features.test.requests.queries;

public class GetTestDetailedDtoRequest: IRequest<RequestTestDto>
{
    public int Id { get; set; }
}