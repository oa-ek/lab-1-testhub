using Application.results.common;

namespace Application.responses.failed;

public class BadRequestFailedStatusResult<T> : BaseCommandResult <T>
{
    public BadRequestFailedStatusResult()
    {
        Success = false;
        Message = "Operation Failed";
    }

    public BadRequestFailedStatusResult(IEnumerable<ValidationFailure> errors) 
    {
        Success = false;
        Message = "Operation Failed";
        Errors = errors.Select(q => q.ErrorMessage).ToList();
    }
    
    public BadRequestFailedStatusResult(string error) 
    {
        Success = false;
        Message = "Operation Failed";
        Errors = new List<string> { error };
    }
}