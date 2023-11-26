using Application.dtos.sharedDTOs;

namespace Application.features.category.requests.queries;

public class GetCategoryListRequest : IRequest<List<CategoryDto>> {}