using System.Runtime.InteropServices.ComTypes;
using Application.features.category.requests.commands;
using Application.persistence.contracts;
using Domain.entities;

namespace Application.features.category.handlers.commands;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(command.CategoryDto);

        category = await _repository.Add(category);
        return category.Id;
    }
}