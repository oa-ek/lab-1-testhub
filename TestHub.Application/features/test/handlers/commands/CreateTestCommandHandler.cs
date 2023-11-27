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
            return new BadRequestFailedStatusResponse<RespondTestDto>(new List<ValidationFailure>
            {
                new ("TestDto", "TestDto cannot be null.")
            });
        
        var validator = new RequestTestDtoValidator();
        var validationResult = await validator.ValidateAsync(command.TestDto, cancellationToken);
        if (!validationResult.IsValid) return new BadRequestFailedStatusResponse<RespondTestDto>(validationResult.Errors);
        
        var test = _mapper.Map<Test>(command.TestDto);

        test = await _repository.Add(test);
        return new CreatedSuccessStatusResponse<RespondTestDto>(test.Id);
    }
}