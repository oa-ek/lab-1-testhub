using Application.dtos.sharedDTOs;
using Application.features.category.requests.commands;
using Application.results.common;

namespace Application.features.category.handlers.commands;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, BaseCommandResult<CategoryDto>>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResult<CategoryDto>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        if (command.CategoryDto == null)
            return new BadRequestFailedStatusResult<CategoryDto>(new List<ValidationFailure>
            {
                new ("CategoryDto", "CategoryDto cannot be null.")
            });

        var validator = new CategoryDtoValidator();
        var validationResult = await validator.ValidateAsync(command.CategoryDto, cancellationToken);
        if (!validationResult.IsValid) return new BadRequestFailedStatusResult<CategoryDto>(validationResult.Errors);

        var category = _mapper.Map<Category>(command.CategoryDto);

        category = await _repository.Add(category);
        return new CreatedSuccessStatusResult<CategoryDto>(category.Id);
    }
}