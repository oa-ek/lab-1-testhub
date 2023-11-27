using Application.features.answer.requests.commands;

namespace Application.features.answer.handlers.commands;

public class UpdateAnswerCommandHandler : IRequestHandler<UpdateAnswerCommand, BaseCommandResponse<RespondAnswerDto>>
{
    private readonly IAnswerRepository _repository;
    private readonly IMapper _mapper;

    public UpdateAnswerCommandHandler(IAnswerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<BaseCommandResponse<RespondAnswerDto>> Handle(UpdateAnswerCommand command, CancellationToken cancellationToken)
    {
        var validator = new RequestAnswerDtoValidator(_repository);
        var validationResult = await validator.ValidateAsync(command.AnswerDto, cancellationToken);
        if (!validationResult.IsValid) return new BadRequestFailedStatusResponse<RespondAnswerDto>(validationResult.Errors);
        
        var answer = await _repository.Get(command.Id);
        if (answer == null) return new NotFoundFailedStatusResponse<RespondAnswerDto>(command.Id);
    
        _mapper.Map(command.AnswerDto, answer);
        await _repository.Update(answer);
        return new OkSuccessStatusResponse<RespondAnswerDto>(answer.Id);
    }
}