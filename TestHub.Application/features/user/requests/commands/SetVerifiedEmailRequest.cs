namespace Application.features.user.requests.commands;

public class SetVerifiedEmailRequest : IRequest<BaseCommandResponse<RespondAuthenticationDto>>
{
    public required VerifiedEmailDto? VerifiedEmailDto { get; set; }
}