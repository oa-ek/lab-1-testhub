using Application.dtos.sharedDTOs;
using Application.features.category.requests.queries;
using Application.results.common;

namespace Application.features.category.handlers.queries;

public class GetCategoryDtoRequestHandler : IRequestHandler<GetCategoryDtoRequest, BaseCommandResult<CategoryDto>>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetCategoryDtoRequestHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<BaseCommandResult<CategoryDto>> Handle(GetCategoryDtoRequest request, CancellationToken cancellationToken)
    {
        var category = await _repository.Get(request.Id);
        if (category == null) return new NotFoundFailedStatusResult<CategoryDto>(request.Id);

        var categoryDto = _mapper.Map<CategoryDto>(category);
        return new OkSuccessStatusResult<CategoryDto>(categoryDto);
    }
}