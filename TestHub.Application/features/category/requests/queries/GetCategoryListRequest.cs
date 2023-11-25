using Application.persistence.dtos;

namespace Application.features.category.requests.queries;

public class GetCategoryListRequest : IRequest<List<CategoryDto>> {}