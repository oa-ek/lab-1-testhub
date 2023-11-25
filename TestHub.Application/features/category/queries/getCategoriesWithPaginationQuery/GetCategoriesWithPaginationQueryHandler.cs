using Application.common.models;
using Application.repositories.interfaces;
using Application.repositories.interfaces.common;
using Domain.entities;

namespace Application.features.category.queries.getCategoriesWithPaginationQuery;

public class
    GetCategoriesWithPaginationQueryHandler : IRequestHandler<GetCategoriesWithPaginationQuery,
        PaginatedList<CategoryDto>>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetCategoriesWithPaginationQueryHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CategoryDto>> Handle(GetCategoriesWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var categoriesQuery = await _repository.GetAllAsync();
        var categoryDtos =
            _mapper.Map<List<CategoryDto>>((categoriesQuery ?? Array.Empty<Category>()).OrderBy(c => c.Title));
        var paginatedCategories =
            await PaginatedList<CategoryDto>.CreateAsync(categoryDtos.AsQueryable(), request.PageNumber,
                request.PageSize);

        return paginatedCategories;
    }
}