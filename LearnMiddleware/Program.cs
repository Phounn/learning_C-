using LearnMiddleware.MiddleComponents;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<MyCustomMiddleware>();
builder.Services.AddTransient<MyCustomExceptionHandler>();

var app = builder.Build();

app.UseMiddleware<MyCustomExceptionHandler>();
//Middle ware #1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("middleware 1 before calling\r\n");
    await next(context);
    await context.Response.WriteAsync("middleware 1 after calling\r\n");
}); // the middleware is worked when it has a request

app.UseMiddleware<MyCustomMiddleware>(); 


app.Map("/employees", (appBuilder) =>
{
    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
    {
        await context.Response.WriteAsync("middleware 5 before calling\r\n");
        await next(context);
        await context.Response.WriteAsync("middleware 5 after calling\r\n");
    });
    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
    {
        await context.Response.WriteAsync("middleware 6 before calling\r\n");
        await next(context);
        await context.Response.WriteAsync("middleware 6 after calling\r\n");
    });
});// the middleware is worked when it directs to the path by branch

app.UseWhen((context) =>
{
    return context.Request.Path.StartsWithSegments("/employees") && context.Request.Query.ContainsKey("id");
},
    (appBuilder) =>
    {
        appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
        {
            await context.Response.WriteAsync("middleware 5 from usewhen before calling\r\n");
            await next(context);
            await context.Response.WriteAsync("middleware 5 from usewhen  after calling\r\n");
        });
        appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
        {
            await context.Response.WriteAsync("middleware 6 from usewhen before calling\r\n");
            await next(context);
            await context.Response.WriteAsync("middleware 6 from usewhen after calling\r\n");
        });
    });// the middleware is worked when it directs to the path by branch but it will rejoin to main branch

app.MapWhen((context) =>
{
    return context.Request.Path.StartsWithSegments("/employees") && context.Request.Query.ContainsKey("id");
},
    (appBuilder) =>
{
    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
    {
        await context.Response.WriteAsync("middleware 5 from mapwhen before calling");
        await next(context);
        await context.Response.WriteAsync("middleware 5 from mapwhen after calling");
    });
    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
    {
        await context.Response.WriteAsync("middleware 6 from mapwhen before calling");
        await next(context);
        await context.Response.WriteAsync("middleware 6 from mapwhen after calling");
    });
});// the middleware is worked when it directs to the path by branch

//Middle ware #2
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    //throw new ApplicationException("Error in middleware 2");
    await context.Response.WriteAsync("middleware 2 before calling\r\n");
    await next(context);
    await context.Response.WriteAsync("middleware 2 after calling\r\n");
});
//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Middleware 2!");
//}); //middleware by using Run()

//Middle ware #3
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("middleware 3 before calling\r\n");
    await next(context);
    await context.Response.WriteAsync("middleware 3 after calling\r\n");
});

app.Run();
