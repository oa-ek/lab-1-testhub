using Application.contracts.persistence;
using Application.features.test.requests.commands;

namespace Application.features.test.handlers.commands;

public class UpdateTestCommandHandler : IRequestHandler<UpdateTestCommand, BaseCommandResponse>
{
    private readonly ITestRepository _repository;
    private readonly IMapper _mapper;

    public UpdateTestCommandHandler(ITestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<BaseCommandResponse> Handle(UpdateTestCommand command, CancellationToken cancellationToken)
    {
        var validator = new RequestTestDtoValidator();
        var validationResult = await validator.ValidateAsync(command.TestDto, cancellationToken);
        if (!validationResult.IsValid) return new BadRequestFailedStatusResponse(validationResult.Errors);
        
        var test = await _repository.Get(command.Id);
        if (test == null) return new NotFoundFailedStatusResponse(command.Id);
        
        _mapper.Map(command.TestDto, test);
        await _repository.Update(test);
        return new OkSuccessStatusResponse(test.Id);
    }
}