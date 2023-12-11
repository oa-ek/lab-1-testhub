using Application.dtos.sharedDTOs;
using Application.results.common;

namespace Application.features.category.requests.queries;

public class GetCategoryDtoRequest : IRequest<BaseCommandResult<CategoryDto>>
{
    public int Id { get; set; }
}