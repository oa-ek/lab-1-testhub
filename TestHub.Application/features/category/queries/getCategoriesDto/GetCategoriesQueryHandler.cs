using Application.repositories.interfaces;

namespace Application.features.category.queries.getCategoriesDto
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesDtoQuery, IEnumerable<CategoryDto>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesDtoQuery request, CancellationToken cancellationToken)
        {
            return (await _repository.GetAllAsync())
                ?.OrderBy(c => c.Title)
                .Select(category => _mapper.Map<CategoryDto>(category))
                .ToList() ?? new List<CategoryDto>();
        }
    }
}