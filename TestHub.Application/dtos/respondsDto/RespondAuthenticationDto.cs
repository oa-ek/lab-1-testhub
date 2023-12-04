namespace Application.dtos.respondsDto;

public class RespondAuthenticationDto
{
    public RespondUserDto RespondUserDto { get; set; } = null!;
    public string? Token { get; init; } = null!;
}