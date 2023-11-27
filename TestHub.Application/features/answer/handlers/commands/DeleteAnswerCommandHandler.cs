using Application.features.answer.requests.commands;

namespace Application.features.answer.handlers.commands;

public class DeleteAnswerCommandHandler : IRequestHandler<DeleteAnswerCommand, BaseCommandResponse<RespondAnswerDto>>
{
    private readonly IAnswerRepository _repository;

    public DeleteAnswerCommandHandler(IAnswerRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<BaseCommandResponse<RespondAnswerDto>> Handle(DeleteAnswerCommand command, CancellationToken cancellationToken)
    {
        var answer = await _repository.Get(command.Id);
        if (answer == null) return new NotFoundFailedStatusResponse<RespondAnswerDto>(command.Id);
        
        await _repository.Delete(answer);
        return new OkSuccessStatusResponse<RespondAnswerDto>(command.Id);
    }
}