using Application.dtos.sharedDTOs;
using Application.features.category.requests.queries;

namespace Application.features.category.handlers.queries;

public class GetCategoryDtoRequestHandler : IRequestHandler<GetCategoryDtoRequest, BaseCommandResponse<CategoryDto>>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetCategoryDtoRequestHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<BaseCommandResponse<CategoryDto>> Handle(GetCategoryDtoRequest request, CancellationToken cancellationToken)
    {
        var category = await _repository.Get(request.Id);
        if (category == null) return new NotFoundFailedStatusResponse<CategoryDto>(request.Id);

        var categoryDto = _mapper.Map<CategoryDto>(category);
        return new OkSuccessStatusResponse<CategoryDto>(categoryDto);
    }
}