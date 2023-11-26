namespace Application.responses.failed;

public class BadRequestFailedStatusResponse : BaseCommandResponse
{
    public BadRequestFailedStatusResponse(IEnumerable<FluentValidation.Results.ValidationFailure> errors) 
    {
        Success = true;
        Message = "Operation Failed";
        Errors = errors.Select(q => q.ErrorMessage).ToList();
    }
}