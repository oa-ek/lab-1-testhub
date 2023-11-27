namespace Application.responses.failed;

public class NotFoundFailedStatusResponse<T> : BaseCommandResponse<T>
{
    public NotFoundFailedStatusResponse(int objectId)
    {
        Success = false;
        Message = "Operation Failed";
        Errors = new List<string>{ "Not Found"};
        ResponseObjectId = objectId;
    }
}