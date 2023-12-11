using Application.dtos.sharedDTOs;
using Application.features.category.requests.commands;
using Application.results.common;

namespace Application.features.category.handlers.commands;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, BaseCommandResult<CategoryDto>>
{
    private readonly ICategoryRepository _repository;

    public DeleteCategoryCommandHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<BaseCommandResult<CategoryDto>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await _repository.Get(command.Id);
        if (category == null) return new NotFoundFailedStatusResult<CategoryDto>(command.Id);

        await _repository.Delete(category);
        return new OkSuccessStatusResult<CategoryDto>(command.Id);
    }
}