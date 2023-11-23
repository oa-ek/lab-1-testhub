using Application.common.models;

namespace Application.features.category.queries.getCategoriesWithPaginationQuery;

public record GetCategoriesWithPaginationQuery : IRequest<PaginatedList<CategoryDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}