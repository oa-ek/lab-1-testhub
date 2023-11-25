using Application.common.models;
using Application.repositories.interfaces;

namespace Application.features.category.queries.getCategoriesDto
{
    public class GetCategoriesDtoQueryHandler : IRequestHandler<GetCategoriesDtoQuery, Result<IEnumerable<CategoryDto>>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public GetCategoriesDtoQueryHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CategoryDto>>> Handle(GetCategoriesDtoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var categories = (await _repository.GetAllAsync())
                    ?.OrderBy(c => c.Title)
                    .Select(category => _mapper.Map<CategoryDto>(category))
                    .ToList() ?? new List<CategoryDto>();
            
                return Result<IEnumerable<CategoryDto>>.Success(categories);
            }
            catch (Exception e)
            {
                return Result<IEnumerable<CategoryDto>>.Failure(new[] { e.Message })!;
            }
        }
    }
}