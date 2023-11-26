using Application.features.question.requests.commands;
using Domain.entities;

namespace Application.features.question.handlers.commands;

public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, BaseCommandResponse>
{
    private readonly IQuestionRepository _repository;
    private readonly IMapper _mapper;

    public CreateQuestionCommandHandler(IQuestionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<BaseCommandResponse> Handle(CreateQuestionCommand command, CancellationToken cancellationToken)
    {
        var validator = new RequestQuestionDtoValidator(_repository);
        var validationResult = await validator.ValidateAsync(command.QuestionDto, cancellationToken);
        if (!validationResult.IsValid) return new BadRequestFailedStatusResponse(validationResult.Errors);
        
        var question = _mapper.Map<Question>(command.QuestionDto);

        question = await _repository.Add(question);
        return new CreatedSuccessStatusResponse(question.Id);
    }
}