namespace Application.features.category.requests.queries;

public class GetCategoryListByTestIdRequest : IRequest<BaseCommandResponse<List<CategoryDto>>>
{
    public int Id { get; set; }
}