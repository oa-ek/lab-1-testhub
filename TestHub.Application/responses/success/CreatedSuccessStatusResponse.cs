namespace Application.responses.success;

public class CreatedSuccessStatusResponse<T> : BaseCommandResponse<T>
{
    public CreatedSuccessStatusResponse(int objectId)
    {
        Success = true;
        Message = "Creation Successful";
        ResponseObjectId = objectId;
    }
}