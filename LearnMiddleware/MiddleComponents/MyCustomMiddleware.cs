
namespace LearnMiddleware.MiddleComponents
{
    public class MyCustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("middleware 1 custom before calling \r\n");
            await next(context);
            await context.Response.WriteAsync("middleware 1 custom after calling \r\n");
        }
    }
}
