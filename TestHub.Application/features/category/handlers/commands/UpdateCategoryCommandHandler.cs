using Application.features.category.requests.commands;
using Application.persistence.contracts;

namespace Application.features.category.handlers.commands;

public class UpdateCategoryCommandHandler: IRequestHandler<UpdateCategoryCommand, Unit>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await _repository.Get(command.Id);
        _mapper.Map(command.CategoryDto, category);

        await _repository.Update(category);
        return Unit.Value;
    }
}