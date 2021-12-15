using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace API.Middleware
{
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _log;

        public ErrorHandler(RequestDelegate next, ILoggerFactory log)
        {
            this._next = next;
            this._log = log.CreateLogger("ErroHandler");
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(httpContext, ex);
            }
        }

        private async Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var errorResponse = new ErroResponseDefault
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = exception.Message
            };

            _log.LogError($"Error: {exception.Message}");
            _log.LogError($"Stack: {exception.StackTrace}");
            _log.LogError($"Stack: {exception.InnerException}");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)errorResponse.StatusCode;

            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}
