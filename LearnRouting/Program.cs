var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) =>
{
    await next(context);
});

app.UseRouting();

app.Use(async (context, next) =>
{
    await next(context);
});
app.UseEndpoints(enpoints =>
{
    enpoints.MapGet("/employee", async (HttpContext context) =>
    {
        await context.Response.WriteAsync("Get Employee");
    });
    enpoints.MapPost("/employee", async (HttpContext context) =>
    {
        await context.Response.WriteAsync("Post Employee");
    });
    enpoints.MapPut("/employee", async (HttpContext context) =>
    {
        await context.Response.WriteAsync("Update Employee");
    });
    enpoints.MapDelete("/employee/{id}", async (HttpContext context) =>
    {
        await context.Response.WriteAsync($"Delete the Employee {context.Request.RouteValues["id"]}");
    });
});

app.Run();
