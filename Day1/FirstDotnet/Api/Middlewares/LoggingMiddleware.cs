namespace Api.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        await httpContext.Response.WriteAsync("  Middleware 2 A  "); // 2
        await _next(httpContext);
        await httpContext.Response.WriteAsync("  Middleware 2 B  "); //5
    }
}

public static class LoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingMiddleware>();
    }
}
