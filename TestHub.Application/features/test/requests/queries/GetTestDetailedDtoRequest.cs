using Application.dtos;

namespace Application.features.test.requests.queries;

public class GetTestDetailedDtoRequest: IRequest<TestDto>
{
    public int Id { get; set; }
}