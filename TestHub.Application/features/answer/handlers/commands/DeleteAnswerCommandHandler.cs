using Application.features.answer.requests.commands;
using Application.results.common;

namespace Application.features.answer.handlers.commands;

public class DeleteAnswerCommandHandler : IRequestHandler<DeleteAnswerCommand, BaseCommandResult<RespondAnswerDto>>
{
    private readonly IAnswerRepository _repository;

    public DeleteAnswerCommandHandler(IAnswerRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<BaseCommandResult<RespondAnswerDto>> Handle(DeleteAnswerCommand command, CancellationToken cancellationToken)
    {
        var answer = await _repository.Get(command.Id);
        if (answer == null) return new NotFoundFailedStatusResult<RespondAnswerDto>(command.Id);
        
        await _repository.Delete(answer);
        return new OkSuccessStatusResult<RespondAnswerDto>(command.Id);
    }
}