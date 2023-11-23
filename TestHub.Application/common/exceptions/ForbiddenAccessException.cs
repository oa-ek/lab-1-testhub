namespace Application.common.exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException()
        : base("Forbidden Access")
    {
    }

    public ForbiddenAccessException(string message)
        : base(message)
    {
    }

    public ForbiddenAccessException(string message, Exception inner)
        : base(message, inner)
    {
    }
}