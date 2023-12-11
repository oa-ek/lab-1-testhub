using Application.results.common;

namespace Application.responses.success;

public class OkSuccessStatusResult<T>  : BaseCommandResult<T>
{
    public OkSuccessStatusResult(int objectId)
    {
        Success = true;
        Message = "Operation Successful";
        ResponseObjectId = objectId;
    }
    
    public OkSuccessStatusResult(T responseObject)
    {
        Success = true;
        Message = "Operation Successful";
        ResponseObject = responseObject;
    }
}