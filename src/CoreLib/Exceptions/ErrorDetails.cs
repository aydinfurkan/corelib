using Newtonsoft.Json;

namespace CoreLib.Exceptions
{
    public class ErrorDetails : System.Exception
    {
        public int StatusCode { get; set; } = 500;
        public int Code { get; set; }
        public string ErrorMessage { get; set; } = "An error occured";
        
        public ErrorDetails(){}

        [JsonConstructor]
        public ErrorDetails(int statusCode, int code, string message)
        {
            StatusCode = statusCode;
            Code = code;
            ErrorMessage = message;
        }
 
        public override string ToString()
        {
            return JsonConvert.SerializeObject(new
            {
                statusCode = StatusCode,
                code = Code,
                message = ErrorMessage,
            });
        }
    }
}