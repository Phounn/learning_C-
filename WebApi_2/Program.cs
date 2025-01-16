using Microsoft.EntityFrameworkCore;
using WebApi_2.Data;
using WebApi_2.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
Configurate(builder.Services, builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();

static void Configurate(IServiceCollection Services, ConfigurationManager config)
{
    Services.AddControllers();
    Services.AddSwaggerGen();
    Services.AddDbContext<DataContext>(options =>
    {
        options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
    });
}
