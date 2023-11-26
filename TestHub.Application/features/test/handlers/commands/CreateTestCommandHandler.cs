using Application.contracts.persistence;
using Application.features.test.requests.commands;
using Domain.entities;

namespace Application.features.test.handlers.commands;

public class CreateTestCommandHandler : IRequestHandler<CreateTestCommand, BaseCommandResponse>
{
    private readonly ITestRepository _repository;
    private readonly IMapper _mapper;

    public CreateTestCommandHandler(ITestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<BaseCommandResponse> Handle(CreateTestCommand command, CancellationToken cancellationToken)
    {
        var validator = new RequestTestDtoValidator();
        var validationResult = await validator.ValidateAsync(command.TestDto, cancellationToken);
        if (!validationResult.IsValid) return new BadRequestFailedStatusResponse(validationResult.Errors);
        
        var test = _mapper.Map<Test>(command.TestDto);

        test = await _repository.Add(test);
        return new CreatedSuccessStatusResponse(test.Id);
    }
}