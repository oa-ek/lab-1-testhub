using Application.features.category.requests.commands;
using Domain.entities;

namespace Application.features.category.handlers.commands;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, BaseCommandResponse>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var validator = new CategoryDtoValidator();
        var validationResult = await validator.ValidateAsync(command.CategoryDto, cancellationToken);
        if (!validationResult.IsValid) return new BadRequestFailedStatusResponse(validationResult.Errors);
        
        var category = _mapper.Map<Category>(command.CategoryDto);

        category = await _repository.Add(category);
        return new CreatedSuccessStatusResponse(category.Id);
    }
}