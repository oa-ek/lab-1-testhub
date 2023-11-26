using Application.dtos.sharedDTOs;
using Application.features.category.requests.queries;
using Application.persistence.contracts;

namespace Application.features.category.handlers.queries;

public class GetCategoryDtoRequestHandler : IRequestHandler<GetCategoryDtoRequest, CategoryDto>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetCategoryDtoRequestHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<CategoryDto> Handle(GetCategoryDtoRequest request, CancellationToken cancellationToken)
    {
        var category = await _repository.Get(request.Id);
        
        return _mapper.Map<CategoryDto>(category);
    }
}