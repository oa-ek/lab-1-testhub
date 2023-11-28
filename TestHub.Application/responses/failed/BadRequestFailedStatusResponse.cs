namespace Application.responses.failed;

public class BadRequestFailedStatusResponse<T> : BaseCommandResponse <T>
{
    public BadRequestFailedStatusResponse()
    {
        Success = false;
        Message = "Operation Failed";
    }

    public BadRequestFailedStatusResponse(IEnumerable<ValidationFailure> errors) 
    {
        Success = false;
        Message = "Operation Failed";
        Errors = errors.Select(q => q.ErrorMessage).ToList();
    }
}