using Domain.entities;

namespace Application.features.category.queries.getCategoriesDto;
public abstract record GetCategoriesDtoQuery : IRequest<Category>, IRequest<IEnumerable<CategoryDto>>;