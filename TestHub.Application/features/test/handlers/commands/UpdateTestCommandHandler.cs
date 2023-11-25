using Application.features.test.requests.commands;
using Application.persistence.contracts;

namespace Application.features.test.handlers.commands;

public class UpdateTestCommandHandler : IRequestHandler<UpdateTestCommand, Unit>
{
    private readonly ITestRepository _repository;
    private readonly IMapper _mapper;

    public UpdateTestCommandHandler(ITestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(UpdateTestCommand command, CancellationToken cancellationToken)
    {
        var test = await _repository.Get(command.Id);
        _mapper.Map(command.TestDto, test);

        await _repository.Update(test);
        return Unit.Value;
    }
}