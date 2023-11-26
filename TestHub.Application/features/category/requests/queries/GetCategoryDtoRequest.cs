using Application.dtos.sharedDTOs;

namespace Application.features.category.requests.queries;

public class GetCategoryDtoRequest : IRequest<CategoryDto>
{
    public int Id { get; set; }
}