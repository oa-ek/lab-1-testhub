using Application.features.category.requests.commands;
using Application.persistence.contracts;

namespace Application.features.category.handlers.commands;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
    private readonly ICategoryRepository _repository;

    public DeleteCategoryCommandHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Unit> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await _repository.Get(command.Id);

        await _repository.Delete(category);
        return Unit.Value;
    }
}