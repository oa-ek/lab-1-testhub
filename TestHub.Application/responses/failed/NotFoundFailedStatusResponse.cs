namespace Application.responses.failed;

public class NotFoundFailedStatusResponse : BaseCommandResponse
{
    public NotFoundFailedStatusResponse(int objectId)
    {
        Success = false;
        Message = "Operation Failed: Not Found";
        ObjectId = objectId;
    }
}