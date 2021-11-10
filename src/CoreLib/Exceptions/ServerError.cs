namespace CoreLib.Exceptions
{
    public class ServerError : ErrorDetails
    {
        public ServerError(string message = null)
        {
            Code = 999;
            StatusCode = 500;
            ErrorMessage = message ?? $"Something went wrong.";
        }
    }
}
