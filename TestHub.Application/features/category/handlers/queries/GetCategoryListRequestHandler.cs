﻿using Application.features.category.requests.queries;
using Application.persistence.contracts;
using Application.persistence.dtos;

namespace Application.features.category.handlers.queries;

public class GetCategoryListRequestHandler : IRequestHandler<GetCategoryListRequest, List<CategoryDto>>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetCategoryListRequestHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<CategoryDto>> Handle(GetCategoryListRequest request, CancellationToken cancellationToken)
    {
        var categories = await _repository.GetAll();

        return _mapper.Map<List<CategoryDto>>(categories);
    }
}