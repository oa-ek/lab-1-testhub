using Application.features.test.requests.commands;

namespace Application.features.test.handlers.commands;

public class CreateTestCommandHandler : IRequestHandler<CreateTestCommand, BaseCommandResponse<RespondTestDto>>
{
    private readonly ITestRepository _repository;
    private readonly IMapper _mapper;

    public CreateTestCommandHandler(ITestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<BaseCommandResponse<RespondTestDto>> Handle(CreateTestCommand command, CancellationToken cancellationToken)
    {
        if (command.TestDto == null)
            return new BadRequestFailedStatusResponse<RespondTestDto>("TestDto cannot be null.");
        
        var validator = new RequestTestDtoValidator();
        var validationResult = await validator.ValidateAsync(command.TestDto, cancellationToken);
        if (!validationResult.IsValid) return new BadRequestFailedStatusResponse<RespondTestDto>(validationResult.Errors);
        
        var test = _mapper.Map<Test>(command.TestDto);
        test.OwnerId = command.OwnerId;
        
        test = await _repository.Add(test);
        
        var requestCategories = command.TestDto.Categories;
        var validationSetCategories = await ProcessCategoriesAsync(test, requestCategories);
        var validationFailures = validationSetCategories as ValidationFailure[] ?? validationSetCategories.ToArray();
        if (validationFailures.Any())
            return new BadRequestFailedStatusResponse<RespondTestDto>(validationFailures);
        
        return new CreatedSuccessStatusResponse<RespondTestDto>(test.Id);
    }
    
    private async Task<IEnumerable<ValidationFailure>> ProcessCategoriesAsync(Test test, IEnumerable<CategoryDto> requestCategories)
    {
        var errors = new List<ValidationFailure>();
        foreach (var categoryDto in requestCategories)
        {
            try
            {
                await _repository.SetCategories(test, categoryDto);
            }
            catch (Exception ex)
            {
                errors.Add(new ValidationFailure("Categories", $"Failed to assign category '{categoryDto.Title}' to the test '{test.Id}'. {ex.InnerException}"));
            }
        }

        return errors;
    }
}