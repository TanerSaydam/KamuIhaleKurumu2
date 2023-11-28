namespace MyFirstWebApi.Middleware;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder CreateMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        return app;
    }
}
