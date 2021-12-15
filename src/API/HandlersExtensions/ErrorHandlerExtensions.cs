using API.Middleware;
using Microsoft.AspNetCore.Builder;

namespace API.HandlersExtensions
{
    public static class ErrorHandlerExtensions
    {
        public static IApplicationBuilder ErrorHandlerExtension(this IApplicationBuilder appBuilder)
        {
            return appBuilder.UseMiddleware<ErrorHandler>();
        }
    }
}
