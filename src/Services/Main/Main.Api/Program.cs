using Main.Api.Extensions;
using Main.Infrastructure.Persistence;
using Main.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

#region Ioc
builder.Services.RegisterServices(builder.Configuration);
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
