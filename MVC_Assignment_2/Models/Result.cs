namespace MVC_Assignment_2.Models
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }

        // Success constructor
        public Result(T data)
        {
            Success = true;
            Data = data;
        }

        // Failure constructor
        public Result(string errorMessage)
        {
            Success = false;
            ErrorMessage = errorMessage;
        }
    }
}
