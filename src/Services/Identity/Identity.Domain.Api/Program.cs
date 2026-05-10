using Identity.Domain.Api.Extensions;
using Identity.Domain.Infra.Data.Context;
using Identity.Domain.Infra.IoC;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

#region IoC
builder.Services.RegisterServices(builder.Configuration, builder.Environment);
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase<Program>();

app.Run();

public partial class Program { }