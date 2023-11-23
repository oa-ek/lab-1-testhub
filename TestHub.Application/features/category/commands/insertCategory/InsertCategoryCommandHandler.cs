using Application.common.models;
using Application.repositories.interfaces;
using Domain.entities;

namespace Application.features.category.commands.insertCategory
{
    public class InsertCategoryCommandHandler : IRequestHandler<InsertCategoryCommand, Result<Category>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public InsertCategoryCommandHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<Category>> Handle(InsertCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request.CategoryDto);

            try
            {
                await _repository.InsertAsync(category);
                return Result<Category>.Success(category);
            }
            catch (Exception e)
            {
                return Result<Category>.Failure(new[] { e.Message })!;
            }
        }
    }
}