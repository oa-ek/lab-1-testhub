using Application.dtos.sharedDTOs;
using Application.results.common;

namespace Application.features.category.requests.queries;

public class GetCategoryListRequest : IRequest<BaseCommandResult<List<CategoryDto>>> {}