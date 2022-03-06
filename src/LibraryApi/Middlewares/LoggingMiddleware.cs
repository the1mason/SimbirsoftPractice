using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LibraryApi.Middlewares
{
    /// <summary>
    /// 2.2.3 - Logging middleware
    /// </summary>
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<LoggingMiddleware>();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            _logger.LogInformation($"[{DateTime.UtcNow}] {httpContext.Connection.RemoteIpAddress} -> {httpContext.Request.Method} {httpContext.Request.Path.Value}");
            await this._next(httpContext);
            _logger.LogInformation($"[{DateTime.UtcNow}] {httpContext.Connection.RemoteIpAddress} <- {httpContext.Request.Method} {httpContext.Request.Path.Value}");

        }
    }

    public static class LoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}
