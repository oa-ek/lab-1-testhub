using Application.dtos.sharedDTOs;

namespace Application.features.category.requests.queries;

public class GetCategoryDtoRequest : IRequest<BaseCommandResponse<CategoryDto>>
{
    public int Id { get; set; }
}