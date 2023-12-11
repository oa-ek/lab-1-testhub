using Application.dtos.sharedDTOs;
using Application.features.category.requests.commands;
using Application.results.common;

namespace Application.features.category.handlers.commands;

public class UpdateCategoryCommandHandler: IRequestHandler<UpdateCategoryCommand, BaseCommandResult<CategoryDto>>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResult<CategoryDto>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        if (command.CategoryDto == null)
            return new BadRequestFailedStatusResult<CategoryDto>(new List<ValidationFailure>
            {
                new ("CategoryDto", "CategoryDto cannot be null.")
            });
        
        var validator = new CategoryDtoValidator();
        var validationResult = await validator.ValidateAsync(command.CategoryDto, cancellationToken);
        if (!validationResult.IsValid) return new BadRequestFailedStatusResult<CategoryDto>(validationResult.Errors);
        
        var category = await _repository.Get(command.Id);
        if (category == null) return new NotFoundFailedStatusResult<CategoryDto>(command.Id);
       
        _mapper.Map(command.CategoryDto, category);
        await _repository.Update(category);
        return new OkSuccessStatusResult<CategoryDto>(category.Id);
    }
}