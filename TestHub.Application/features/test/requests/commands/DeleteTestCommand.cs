using Application.results.common;

namespace Application.features.test.requests.commands;

public class DeleteTestCommand : IRequest<BaseCommandResult<RespondTestDto>>
{
    public int Id { get; set; }
}