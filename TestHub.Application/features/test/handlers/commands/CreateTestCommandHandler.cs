using Application.features.test.requests.commands;
using Application.persistence.contracts;
using Domain.entities;

namespace Application.features.test.handlers.commands;

public class CreateTestCommandHandler : IRequestHandler<CreateTestCommand, int>
{
    private readonly ITestRepository _repository;
    private readonly IMapper _mapper;

    public CreateTestCommandHandler(ITestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(CreateTestCommand command, CancellationToken cancellationToken)
    {
        var test = _mapper.Map<Test>(command.TestDto);

        test = await _repository.Add(test);
        return test.Id;
    }
}