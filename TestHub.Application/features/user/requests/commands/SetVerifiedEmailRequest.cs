using Application.results.common;

namespace Application.features.user.requests.commands;

public class SetVerifiedEmailRequest : IRequest<BaseCommandResult<RespondAuthenticationDto>>
{
    public required VerifiedEmailDto? VerifiedEmailDto { get; set; }
}