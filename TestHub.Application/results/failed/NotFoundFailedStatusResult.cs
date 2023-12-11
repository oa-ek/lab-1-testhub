using Application.results.common;

namespace Application.responses.failed;

public class NotFoundFailedStatusResult<T> : BaseCommandResult<T>
{
    public NotFoundFailedStatusResult(int objectId)
    {
        Success = false;
        Message = "Operation Failed";
        Errors = new List<string>{ "Not Found"};
        ResponseObjectId = objectId;
    }
    
    public NotFoundFailedStatusResult(T responseObject)
    {
        Success = false;
        Message = "Operation Failed";
        Errors = new List<string>{ "Not Found"};
        ResponseObject = responseObject;
    }
    
    public NotFoundFailedStatusResult(string property)
    {
        Success = false;
        Message = "Operation Failed";
        Errors = new List<string>{ $"Object with property: {property} was not found in database"};
    }
}