using Application.contracts.persistence;
using Application.features.category.requests.commands;

namespace Application.features.category.handlers.commands;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, BaseCommandResponse>
{
    private readonly ICategoryRepository _repository;

    public DeleteCategoryCommandHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<BaseCommandResponse> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await _repository.Get(command.Id);
        if (category == null) return new NotFoundFailedStatusResponse(command.Id);

        await _repository.Delete(category);
        return new OkSuccessStatusResponse(command.Id);
    }
}