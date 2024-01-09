using Microsoft.Extensions.Configuration;
using Serilog;

namespace AspNetCoreLogging;

public static class LoggerConfigurationExtension
{
    public static LoggerConfiguration StandardAspConfiguration(this LoggerConfiguration cfg, IConfiguration appCfg)
    {
        return cfg.ReadFrom
            .Configuration(appCfg)
            .Enrich.With<ActivityEnricher>()
            .Enrich.FromLogContext();
    }
}