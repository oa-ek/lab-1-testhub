using Application.features.question.requests.commands;
using Application.persistence.contracts;
using Domain.entities;

namespace Application.features.question.handlers.commands;

public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, int>
{
    private readonly IQuestionRepository _repository;
    private readonly IMapper _mapper;

    public CreateQuestionCommandHandler(IQuestionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(CreateQuestionCommand command, CancellationToken cancellationToken)
    {
        var question = _mapper.Map<Question>(command.QuestionDto);

        question = await _repository.Add(question);
        return question.Id;
    }
}