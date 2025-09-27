using Microsoft.EntityFrameworkCore;
using Planner.Core;
using Planner.Core.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

var connectionString =  builder.Configuration.GetConnectionString("Database") ?? throw new Exception("No connection string found");
builder.Services.AddPlannerCore(connectionString);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseSwaggerUI(x => x.SwaggerEndpoint("/openapi/v1.json", "Planner API v1"));

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
