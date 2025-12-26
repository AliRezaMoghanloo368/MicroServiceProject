using Logs.Core.Contracts.Persistence;
using Logs.Infrastructure.Persistence;
using Logs.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

#region IoC
builder.Services.AddScoped<ILogsContext, LogsContext>();
builder.Services.AddScoped<IHistoryRepository, HistoryRepository>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
