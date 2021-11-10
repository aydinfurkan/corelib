namespace CoreLib.Exceptions
{
    public class NotFound : ErrorDetails
    {
        public NotFound(int code, string message = null)
        {
            Code = code;
            StatusCode = 404;
            ErrorMessage = message ?? $"Not Found.";
        }
    }
}