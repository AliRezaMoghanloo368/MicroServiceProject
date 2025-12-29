using Logs.Core.Contracts.Persistence;
using Logs.Grpc.Mapping;
using Logs.Grpc.Services;
using Logs.Infrastructure.Persistence;
using Logs.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
#region IoC
builder.Services.AddScoped<ILogsContext, LogsContext>();
builder.Services.AddScoped<IHistoryRepository, HistoryRepository>();
builder.Services.AddAutoMapper(typeof(GrpcMapping).Assembly);
#endregion
// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<HistoryGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
