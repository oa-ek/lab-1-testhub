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
    
    public NotFoundFailedStatusResponse(T responseObject)
    {
        Success = false;
        Message = "Operation Failed";
        Errors = new List<string>{ "Not Found"};
        ResponseObject = responseObject;
    }
    
    public NotFoundFailedStatusResponse(string property)
    {
        Success = false;
        Message = "Operation Failed";
        Errors = new List<string>{ $"Object with property: {property} was not found in database"};
    }
}