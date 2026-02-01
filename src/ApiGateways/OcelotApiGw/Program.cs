using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddOcelot()
//                 .AddCacheManager(x =>
//                 {
//                     x.WithDictionaryHandle();
//                 });

// ---------- App Configuration ----------
builder.Configuration
    .AddJsonFile(
        $"ocelot.{builder.Environment.EnvironmentName}.json",
        optional: true,
        reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration);

// ---------- Logging ----------
builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(
    builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

await app.UseOcelot();

app.Run();
