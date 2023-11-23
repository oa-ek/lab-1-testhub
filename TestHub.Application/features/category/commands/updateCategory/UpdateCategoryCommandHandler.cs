using Application.common.models;
using Application.repositories.interfaces;
using Domain.entities;

namespace Application.features.category.commands.updateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<Category>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<Category>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryId = request.id;
            var updatedCategory = _mapper.Map<Category>(request.CategoryDto);

            try
            {
                var existingCategory = await _repository.GetByIdAsync(categoryId);
                
                if (existingCategory == null)
                    return Result<Category>.Failure(new[] { "Category not found" })!;
                
                existingCategory.Title = updatedCategory.Title;
                await _repository.UpdateAsync(existingCategory);
                
                return Result<Category>.Success(existingCategory);
            }
            catch (Exception e)
            {
                return Result<Category>.Failure(new[] { "An error occurred while updating the category: " + e.Message })!;
            }
        }
    }
}