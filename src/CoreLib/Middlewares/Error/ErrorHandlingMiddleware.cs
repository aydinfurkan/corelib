using System.Threading.Tasks;
using CoreLib.Exceptions;
using Microsoft.AspNetCore.Http;

namespace CoreLib.Middlewares.Error
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (ErrorDetails ex)
            {
                await HandleAsync(context, ex);
            }
            catch (System.Exception ex)
            {
                await HandleAsync(context, ex);
            }
        }

        private async Task HandleAsync(HttpContext context, ErrorDetails exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = exception.StatusCode;
            await response.WriteAsync(exception.ToString());
        }
        
        private async Task HandleAsync(HttpContext context, System.Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = 500;
            await response.WriteAsync(new ServerError(exception.Message).ToString());
        }
    }
}