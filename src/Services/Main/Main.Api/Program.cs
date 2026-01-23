using Logs.Grpc.Protos;
using Main.Api.Extensions;
using Main.Api.Grpc.Services;
using Main.Api.Mapping;
using Main.Infrastructure.Persistence;
using Main.IoC;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

#region Ioc
builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(MainMappingProfile).Assembly);
#endregion

#region Grpc Ioc
builder.Services.AddGrpcClient<HistoryService.HistoryServiceClient>
    (options =>
    {
        options.Address = new Uri(builder.Configuration["GrpcSettings:HistoryUrl"]);
    });
builder.Services.AddScoped<Logs_HistoryGrpcService>();
#endregion

#region EventBus::Rabbitmq Ioc
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, conf) =>
    {
        conf.Host(builder.Configuration.GetValue<string>("EventBusSettings:HostAddress"));
    });
});
builder.Services.AddMassTransitHostedService();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MigrateDatabase<MainContext>((context, services) =>
{
    var logger = services.GetService<ILogger<MainContextSeed>>();
    MainContextSeed.SeedAsync(context, logger).Wait();
});

app.Run();
