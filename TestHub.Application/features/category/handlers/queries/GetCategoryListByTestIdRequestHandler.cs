using Application.features.category.requests.queries;
using Application.results.common;

namespace Application.features.category.handlers.queries;

public class GetCategoryListByTestIdRequestHandler : IRequestHandler<GetCategoryListByTestIdRequest, BaseCommandResult<List<CategoryDto>>>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetCategoryListByTestIdRequestHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResult<List<CategoryDto>>> Handle(GetCategoryListByTestIdRequest request, CancellationToken cancellationToken)
    {
        var categories = await _repository.GetByTestId(request.Id);

        var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
        return new OkSuccessStatusResult<List<CategoryDto>>(categoryDtos);
    }
}