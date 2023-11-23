using Application.common.models;
using Application.repositories.interfaces;
using Domain.entities;

namespace Application.features.category.commands.deleteCategory;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<Category>>
{
    private readonly ICategoryRepository _repository;

    public DeleteCategoryCommandHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Category>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(request.Id);

        try
        {
            await _repository.DeleteAsync(category);
            return Result<Category>.Success(category);
        }
        catch (Exception e)
        {
            return Result<Category>.Failure(new[] { e.Message })!;
        }
    }
}