using Application.features.question.requests.commands;
using Application.persistence.contracts;

namespace Application.features.question.handlers.commands;

public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, Unit>
{
    private readonly IQuestionRepository _repository;
    private readonly IMapper _mapper;

    public UpdateQuestionCommandHandler(IQuestionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(UpdateQuestionCommand command, CancellationToken cancellationToken)
    {
        var question = await _repository.Get(command.Id);
        _mapper.Map(command.QuestionDto, question);

        await _repository.Update(question);
        return Unit.Value;
    }
}