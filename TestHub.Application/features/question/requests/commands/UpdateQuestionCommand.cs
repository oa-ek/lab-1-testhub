using Application.dtos.requestsDto;
using Application.results.common;

namespace Application.features.question.requests.commands;

public class UpdateQuestionCommand : IRequest<BaseCommandResult<RespondQuestionDto>>
{
    public int Id { get; set; }
    public required RequestQuestionDto? QuestionDto { get; set; }
}