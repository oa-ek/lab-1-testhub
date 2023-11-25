using Application.features.answer.requests.commands;
using Application.persistence.contracts;

namespace Application.features.answer.handlers.commands;

public class UpdateAnswerCommandHandler : IRequestHandler<UpdateAnswerCommand, Unit>
{
    private readonly IAnswerRepository _repository;
    private readonly IMapper _mapper;

    public UpdateAnswerCommandHandler(IAnswerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(UpdateAnswerCommand command, CancellationToken cancellationToken)
    {
        var answer = await _repository.Get(command.Id);
        _mapper.Map(command.AnswerDto, answer);

        await _repository.Update(answer);
        return Unit.Value;
    }
}