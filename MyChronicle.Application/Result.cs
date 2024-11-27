namespace MyChronicle.Application
{
    public enum ErrorCategory
    {
        Other,
        NotFound
    }
    public class ErrorMessage
    {
        public required string Message { get; set; }
        public ErrorCategory Category { get; set; }
    }
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T? Value { get; set; }
        public ErrorMessage? ErrorMsg { get; set; }
        public static Result<T> Success(T value)
        {
            return new Result<T> { IsSuccess = true, Value = value };
        }
        public static Result<T> Failure(string errorMsg, ErrorCategory category = ErrorCategory.Other)
        {
            return new Result<T> { IsSuccess = false, ErrorMsg = new ErrorMessage { Message = errorMsg, Category = category } };
        }
    }
}
