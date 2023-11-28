using Newtonsoft.Json;

namespace MyFirstWebApi.Middleware;

public sealed class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
		try
		{
			await next(context);
		}
		catch (Exception ex)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = StatusCodes.Status500InternalServerError;

			if(ex.GetType() == typeof(ArgumentException))
			{
				context.Response.StatusCode = 503;
			}

			var errorObje = new
			{
				Message = ex.Message,
			};

			string errorString = JsonConvert.SerializeObject(errorObje);

			await context.Response.WriteAsync(errorString);
		}
    }
}
