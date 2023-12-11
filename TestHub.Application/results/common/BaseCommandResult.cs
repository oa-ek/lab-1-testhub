namespace Application.results.common;

public class BaseCommandResult<T>
{
    public bool Success { get; protected init; } = true;
    public string Message { get; protected init; } = string.Empty;
    public List<string>? Errors { get; protected init; }
    public int? ResponseObjectId { get; protected init; }
    public T? ResponseObject { get; protected init; }
}