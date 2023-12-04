namespace Application.dtos.respondsDto;

public class RespondUserDto
{
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public bool IsEmailVerified { get; set; }

}