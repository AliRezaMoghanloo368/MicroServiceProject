namespace SharedLibrary.Patterns.ResultPattern
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Error { get; set; }

        public static Result<T> SuccessResult(T data, string message = "")
        {
            return new Result<T>
            {
                Data = data,
                Success = true,
                Message = message
            };
        }

        public static Result<T> ErrorResult(List<string> errors, string message = "شما یک خطای پیش بینی نشده دارید!")
        {
            return new Result<T>
            {
                Success = false,
                Message = message,
                Error = errors
            };
        }

        public static Result<T> ErrorResult(string error, string message = "شما یک خطای پیش بینی نشده دارید!")
        {
            return new Result<T>
            {
                Success = false,
                Message = message,
                Error = new List<string>() { error }
            };
        }
    }
}


