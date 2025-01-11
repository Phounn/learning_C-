
namespace LearnMiddleware.MiddleComponents
{
    public class MyCustomExceptionHandler : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                context.Response.ContentType = "text/html";
                //context.Response.Headers["Content-Type"] = "text/html";
                await next(context);

            }catch(Exception ex)
            {
                await context.Response.WriteAsync($"<h2>Error: {ex.Message}</h2>");
            }
        }
    }
}
