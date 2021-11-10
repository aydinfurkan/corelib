using System;
using System.Threading.Tasks;
using CoreLib.Exceptions;
using CoreLib.Middlewares.Error;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoreLib.Middlewares.Logging
{
    public class ErrorLoggingMiddleware
    {
        private readonly ILogger<ErrorLoggingMiddleware> _logger;
        private readonly RequestDelegate _next;
        private readonly string _domain;

        public ErrorLoggingMiddleware(RequestDelegate next, ILogger<ErrorLoggingMiddleware> logger, string domain)
        {
            _next = next;
            _logger = logger;
            _domain = domain;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ErrorDetails ex)
            {
                _logger.LogInformation(JsonConvert.SerializeObject(new LogMessage(ex.ToString(), _domain), Formatting.None));
                throw;
            }
            catch (System.Exception e)
            {
                var innerException = GetInnerMostException(e);
                if (innerException != null)
                    _logger.LogError(JsonConvert.SerializeObject(new LogMessage(innerException.ToString(), _domain), Formatting.None));
                else
                {
                    _logger.LogError(JsonConvert.SerializeObject(new LogMessage(e.ToString(), _domain), Formatting.None));
                }

                throw;
            }
        }

        private static System.Exception GetInnerMostException(System.Exception ex)
        {
            var currentEx = ex;
            while (currentEx.InnerException != null)
            {
                currentEx = currentEx.InnerException;
            }

            return currentEx;
        }

        public class LogMessage
        {
            public Guid MessageId { get; }
            public string Domain { get; set; }
            public string Message { get; set; }
            public DateTime Timestamp { get; }

            public LogMessage()
            {
                MessageId = Guid.NewGuid();
                Timestamp = DateTime.Now;
            }

            public LogMessage(string message, string domain) : this()
            {
                Message = message;
                Domain = domain;
            }
        }
    }
}