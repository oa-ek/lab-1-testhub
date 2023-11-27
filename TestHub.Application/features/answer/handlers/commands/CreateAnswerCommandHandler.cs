using Application.features.answer.requests.commands;

namespace Application.features.answer.handlers.commands;

public class CreateAnswerCommandHandler : IRequestHandler<CreateAnswerCommand, BaseCommandResponse<RespondAnswerDto>>
{
    private readonly IAnswerRepository _repository;
    private readonly IMapper _mapper;

    public CreateAnswerCommandHandler(IAnswerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<BaseCommandResponse<RespondAnswerDto>> Handle(CreateAnswerCommand command, CancellationToken cancellationToken)
    {
        var validator = new RequestAnswerDtoValidator(_repository);
        var validationResult = await validator.ValidateAsync(command.AnswerDto, cancellationToken);
        if (!validationResult.IsValid) return new BadRequestFailedStatusResponse<RespondAnswerDto>(validationResult.Errors);
        
        var answer = _mapper.Map<Answer>(command.AnswerDto);

        answer = await _repository.Add(answer);
        return new CreatedSuccessStatusResponse<RespondAnswerDto>(answer.Id);
    }
}