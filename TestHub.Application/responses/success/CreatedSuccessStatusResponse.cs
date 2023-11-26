namespace Application.responses.success;

public class CreatedSuccessStatusResponse : BaseCommandResponse
{
    public CreatedSuccessStatusResponse(int objectId)
    {
        Success = true;
        Message = "Creation Successful";
        ObjectId = objectId;
    }
}