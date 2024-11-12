namespace MyChronicle.Application
{
    public class Result<T>
    {
        public bool IsSuccess {  get; set; }
        public T Value { get; set; }
        public string ErrorMsg { get; set; }
        public static Result<T> Success(T value)
        {
            return new Result<T> { IsSuccess = true, Value = value };
        }
        public static Result<T> Failure(string errorMsg)
        {
            return new Result<T> { IsSuccess = false, ErrorMsg = errorMsg };
        }
    }
}
