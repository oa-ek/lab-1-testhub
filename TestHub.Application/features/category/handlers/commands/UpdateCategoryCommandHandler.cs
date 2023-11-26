using Application.contracts.persistence;
using Application.features.category.requests.commands;

namespace Application.features.category.handlers.commands;

public class UpdateCategoryCommandHandler: IRequestHandler<UpdateCategoryCommand, BaseCommandResponse>
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var validator = new CategoryDtoValidator();
        var validationResult = await validator.ValidateAsync(command.CategoryDto, cancellationToken);
        if (!validationResult.IsValid) return new BadRequestFailedStatusResponse(validationResult.Errors);
        
        var category = await _repository.Get(command.Id);
        if (category == null) return new NotFoundFailedStatusResponse(command.Id);
       
        _mapper.Map(command.CategoryDto, category);
        await _repository.Update(category);
        return new OkSuccessStatusResponse(category.Id);
    }
}