using LoggingCommonConfiguration;
using AspNetCoreLogging;
using Serilog;
using Serilog.Events;
using DotnetLogging.AspNetCoreSerilogExtension;

var builder = WebApplication.CreateBuilder(args);

// Конфигурация
var environment = EnvironmentExtension.GetDonetCoreEnvironmentVarible();
builder.Configuration
    .AddConfigurationFromJsonFile(environment)
    .AddEnvironmentVariables();

builder.Logging.Configure(options =>
{
    options.ActivityTrackingOptions = options.ActivityTrackingOptions = ActivityTrackingOptions.TraceId | ActivityTrackingOptions.SpanId;
});


Log.Logger = new LoggerConfiguration()
    .StandardAspConfiguration(builder.Configuration)
    .CreateBootstrapLogger();

Log.Logger.Debug("Logger initialized");

builder.Host.UseSerilog((ctx, cfg) =>
{
    cfg.StandardAspConfiguration(builder.Configuration);
});

builder.Services.AddControllers();

// Трассировка
var uriFilteringMasks =
    builder.Configuration
        .SerilogConnectionStrings()
        .Select(x => $".*{x.Host}.*")
        .ToList();

var appName = builder.Configuration.GetValue<string>("appName");
if (string.IsNullOrEmpty(appName))
{
    Log.Logger.Warning($"{nameof(appName)} is null or empty");
}

builder.Services.AddOpenTelemetry(
    builder.Configuration.GetValue<Uri>("Otlp:Endpoint"),
    appName,
    uriFilteringMasks,
    Log.Logger);

var app = builder.Build();

app.UseSerilogRequestLogging(options =>
{
    options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;
});

app.UseRouting();
app.MapControllers();
app.Run();