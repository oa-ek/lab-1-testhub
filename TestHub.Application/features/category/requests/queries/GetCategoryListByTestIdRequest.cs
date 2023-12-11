using Application.results.common;

namespace Application.features.category.requests.queries;

public class GetCategoryListByTestIdRequest : IRequest<BaseCommandResult<List<CategoryDto>>>
{
    public int Id { get; set; }
}