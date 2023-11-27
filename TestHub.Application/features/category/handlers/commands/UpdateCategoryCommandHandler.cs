using Application.dtos.sharedDTOs;
using Application.features.category.requests.commands;

namespace Application.features.category.handlers.commands;

public class UpdateCategoryCommandHandler: IRequestHandler<UpdateCategoryCommand, BaseCommandResponse<CategoryDto>>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse<CategoryDto>> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        if (command.CategoryDto == null)
            return new BadRequestFailedStatusResponse<CategoryDto>(new List<ValidationFailure>
            {
                new ("CategoryDto", "CategoryDto cannot be null.")
            });
        
        var validator = new CategoryDtoValidator();
        var validationResult = await validator.ValidateAsync(command.CategoryDto, cancellationToken);
        if (!validationResult.IsValid) return new BadRequestFailedStatusResponse<CategoryDto>(validationResult.Errors);
        
        var category = await _repository.Get(command.Id);
        if (category == null) return new NotFoundFailedStatusResponse<CategoryDto>(command.Id);
       
        _mapper.Map(command.CategoryDto, category);
        await _repository.Update(category);
        return new OkSuccessStatusResponse<CategoryDto>(category.Id);
    }
}