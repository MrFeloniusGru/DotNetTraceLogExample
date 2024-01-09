using Microsoft.Extensions.Configuration;

namespace LoggingCommonConfiguration;

public static class ConfigurationBuilderExtension
{
    /// <summary>
    /// Возвращает конфигурацию из appsettings.json и appsettings.{environment}.json
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="environment"></param>
    /// <returns></returns>
    public static IConfigurationBuilder AddConfigurationFromJsonFile(this IConfigurationBuilder builder, string environment)
    {
        var environmentFileName = $"appsettings.{environment}.json";
        return builder
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile(environmentFileName, optional: true);
    }
}