using Application.results.common;

namespace Application.responses.success;

public class CreatedSuccessStatusResult<T> : BaseCommandResult<T>
{
    public CreatedSuccessStatusResult(int objectId)
    {
        Success = true;
        Message = "Creation Successful";
        ResponseObjectId = objectId;
    }
    
    public CreatedSuccessStatusResult(T responseObject)
    {
        Success = true;
        Message = "Creation Successful";
        ResponseObject = responseObject;
    }
}