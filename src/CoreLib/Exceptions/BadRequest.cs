namespace CoreLib.Exceptions
{
    public class BadRequest : ErrorDetails
    {
        public BadRequest(int code, string key = null, string variable = null, string message = null)
        {
            Code = code;
            StatusCode = 400;
            ErrorMessage = message ?? $"The {key} is not valid : {variable}";
        }
    }
}