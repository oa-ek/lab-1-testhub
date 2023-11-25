using Application.dtos;
using Application.dtos.respondsDto;

namespace Application.features.test.requests.queries;

public class GetTestDetailedDtoRequest: IRequest<RespondTestDto>
{
    public int Id { get; set; }
}