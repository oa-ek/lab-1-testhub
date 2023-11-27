using Application.dtos.sharedDTOs;
using Application.features.category.requests.commands;

namespace Application.features.category.handlers.commands;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, BaseCommandResponse<CategoryDto>>
{
    private readonly ICategoryRepository _repository;

    public DeleteCategoryCommandHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<BaseCommandResponse<CategoryDto>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await _repository.Get(command.Id);
        if (category == null) return new NotFoundFailedStatusResponse<CategoryDto>(command.Id);

        await _repository.Delete(category);
        return new OkSuccessStatusResponse<CategoryDto>(command.Id);
    }
}