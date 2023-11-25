using Application.common.models;
using Domain.entities;

namespace Application.features.category.queries.getCategoriesDto;
public record GetCategoriesDtoQuery : IRequest<Category>, IRequest<Result<CategoryDto>>, IRequest<Result<IEnumerable<CategoryDto>>>;