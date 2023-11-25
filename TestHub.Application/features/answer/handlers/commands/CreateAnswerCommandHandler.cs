using Application.features.answer.requests.commands;
using Application.persistence.contracts;
using Domain.entities;

namespace Application.features.answer.handlers.commands;

public class CreateAnswerCommandHandler : IRequestHandler<CreateAnswerCommand, int>
{
    private readonly IAnswerRepository _repository;
    private readonly IMapper _mapper;

    public CreateAnswerCommandHandler(IAnswerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(CreateAnswerCommand command, CancellationToken cancellationToken)
    {
        var answer = _mapper.Map<Answer>(command.AnswerDto);

        answer = await _repository.Add(answer);
        return answer.Id;
    }
}