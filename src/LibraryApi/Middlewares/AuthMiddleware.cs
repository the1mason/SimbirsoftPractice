using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Middlewares
{
    /// <summary>
    /// 2.2.4 Basic auth middleware
    /// </summary>
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<AuthMiddleware> _logger;

        public AuthMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<AuthMiddleware>();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            
            if (!httpContext.Request.Headers.Keys.Contains("Authorization"))
            {
                _logger.LogInformation($"[{DateTime.UtcNow}] {httpContext.Connection.RemoteIpAddress} -> 401 (no token provided)");
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync("Only for authorized users");
                return;
            }
            //hardcoded auth
            else if (httpContext.Request.Headers["Authorization"] != "Basic admin:admin")
            {
                _logger.LogInformation($"[{DateTime.UtcNow}] {httpContext.Connection.RemoteIpAddress} -> 400 (bad authorization)");
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync("Authorization failed!");
                return;
            }

            _logger.LogInformation($"[{DateTime.UtcNow}] {httpContext.Connection.RemoteIpAddress} -> Authorization completed");
            await this._next(httpContext);
        }
    }

    public static class AuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuth(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}
