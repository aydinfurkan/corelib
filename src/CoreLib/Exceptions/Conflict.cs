namespace CoreLib.Exceptions
{
    public class Conflict : ErrorDetails
    {
        public Conflict(int code, string message = null)
        {
            Code = code;
            StatusCode = 409;
            ErrorMessage = message ?? $"Conflict.";
        }
    }
}