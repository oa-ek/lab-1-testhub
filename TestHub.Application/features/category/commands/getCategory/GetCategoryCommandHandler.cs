using Application.common.models;
using Application.repositories.interfaces;
using Domain.entities;

namespace Application.features.category.commands.getCategory;

public class GetCategoryCommandHandler: IRequestHandler<GetCategoryCommand, Result<Category>>
{
    private readonly ICategoryRepository _repository;

    public GetCategoryCommandHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Category>> Handle(GetCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var category = await _repository.GetByIdAsync(request.Id);
            return Result<Category>.Success(category!);
        }
        catch (Exception e)
        {
            return Result<Category>.Failure(new[] { e.Message })!;
        }
    }
}