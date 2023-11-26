namespace Application.responses.common;

public class BaseCommandResponse
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public List<string>? Errors { get; set; }
    public int? ObjectId { get; set; }
}