using EventBus.Messages.Common;
using Logs.Api.EventBusConsumer;
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
builder.Services.AddAutoMapper(typeof(LogsMappingProfiler).Assembly);
#endregion

#region EventBus::Rabbitmq Ioc
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<LogsHistoryConsumer>();
    config.UsingRabbitMq((ctx, conf) =>
    {
        conf.Host(builder.Configuration.GetValue<string>("EventBusSettings:HostAddress"));
        conf.ReceiveEndpoint(EventBusConstants.LogsHistoryQueue, c =>
        {
            c.ConfigureConsumer<LogsHistoryConsumer>(ctx);
        });
    });
});
builder.Services.AddMassTransitHostedService();
builder.Services.AddScoped<LogsHistoryConsumer>();
//services.AddMassTransit(config =>
//{
//    // ثبت خودکار همه Consumer ها در این Assembly
//    config.AddConsumers(typeof(Program).Assembly);

//    // فرمت نام Queue ها
//    config.SetEndpointNameFormatter(
//        new KebabCaseEndpointNameFormatter("logs", includeNamespace: false)
//    );

//    config.UsingRabbitMq((ctx, conf) =>
//    {
//        conf.Host(configuration.GetValue<string>("EventBusSettings:HostAddress"));

//        // ساخت خودکار Queue برای همه Consumer ها
//        conf.ConfigureEndpoints(ctx);
//    });
//});

//services.AddMassTransitHostedService();
//builder.Services.AddScoped<LogsHistoryConsumer>();
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
