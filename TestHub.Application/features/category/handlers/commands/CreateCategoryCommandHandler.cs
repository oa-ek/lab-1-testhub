using Application.dtos.sharedDTOs;
using Application.features.category.requests.commands;

namespace Application.features.category.handlers.commands;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, BaseCommandResponse<CategoryDto>>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse<CategoryDto>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        if (command.CategoryDto == null)
            return new BadRequestFailedStatusResponse<CategoryDto>(new List<ValidationFailure>
            {
                new ("CategoryDto", "CategoryDto cannot be null.")
            });

        var validator = new CategoryDtoValidator();
        var validationResult = await validator.ValidateAsync(command.CategoryDto, cancellationToken);
        if (!validationResult.IsValid) return new BadRequestFailedStatusResponse<CategoryDto>(validationResult.Errors);

        var category = _mapper.Map<Category>(command.CategoryDto);

        category = await _repository.Add(category);
        return new CreatedSuccessStatusResponse<CategoryDto>(category.Id);
    }
}