using Application.results.common;

namespace Application.features.question.requests.commands;

public class DeleteQuestionCommand : IRequest<BaseCommandResult<RespondQuestionDto>>
{
    public int Id { get; set; }
}