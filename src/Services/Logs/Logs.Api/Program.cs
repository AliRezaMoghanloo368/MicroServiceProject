using Logs.Core.Contracts.Persistence;
using Logs.Core.Mapping;
using Logs.Infrastructure.Persistence;
using Logs.Infrastructure.Repositories;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

#region IoC
builder.Services.AddScoped<ILogsContext, LogsContext>();
builder.Services.AddScoped<IHistoryRepository, HistoryRepository>();
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, conf) =>
    {
        conf.Host(builder.Configuration.GetValue<string>("EventBusSettings:HostAddress"));
    });
});
builder.Services.AddMassTransitHostedService();
builder.Services.AddAutoMapper(typeof(LogsMappingProfiler).Assembly);
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
