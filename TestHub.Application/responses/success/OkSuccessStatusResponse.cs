namespace Application.responses.success;

public class OkSuccessStatusResponse  : BaseCommandResponse
{
    public OkSuccessStatusResponse(int objectId)
    {
        Success = true;
        Message = "Operation Successful";
        ObjectId = objectId;
    }
}