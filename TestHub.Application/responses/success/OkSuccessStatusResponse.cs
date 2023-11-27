namespace Application.responses.success;

public class OkSuccessStatusResponse<T>  : BaseCommandResponse<T>
{
    public OkSuccessStatusResponse(int objectId)
    {
        Success = true;
        Message = "Operation Successful";
        ResponseObjectId = objectId;
    }
    
    public OkSuccessStatusResponse(T responseObject)
    {
        Success = true;
        Message = "Operation Successful";
        ResponseObject = responseObject;
    }
}