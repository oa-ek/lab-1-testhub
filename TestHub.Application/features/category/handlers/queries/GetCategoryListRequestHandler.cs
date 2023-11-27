using Application.dtos.sharedDTOs;
using Application.features.category.requests.queries;

namespace Application.features.category.handlers.queries;

public class GetCategoryListRequestHandler : IRequestHandler<GetCategoryListRequest, BaseCommandResponse<List<CategoryDto>>>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetCategoryListRequestHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse<List<CategoryDto>>> Handle(GetCategoryListRequest request, CancellationToken cancellationToken)
    {
        var categories = await _repository.GetAll();

        var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
        return new OkSuccessStatusResponse<List<CategoryDto>>(categoryDtos);
    }
}