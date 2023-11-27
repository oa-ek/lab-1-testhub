namespace Application.responses.common;

public class BaseCommandResponse<T>
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public List<string>? Errors { get; set; }
    public int? ResponseObjectId { get; set; }
    public T? ResponseObject { get; set; }
}