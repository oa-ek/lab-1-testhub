namespace Application.common.models
{
    public class Result<T>
    {
        private Result(bool succeeded, IEnumerable<string> errors, T? value)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
            Value = value;
        }

        public bool Succeeded { get; init; }
        public string[] Errors { get; init; }
        public T? Value { get; init; }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, Enumerable.Empty<string>(), value);
        }

        public static Result<T> Failure(IEnumerable<string> errors)
        {
            return new Result<T>(false, errors, default);
        }
    }
}