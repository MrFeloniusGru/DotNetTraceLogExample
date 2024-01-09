namespace LoggingCommonConfiguration;

public static class EnvironmentExtension
{
    /// <summary>
    /// Возвращает значение окружения из переменных окружения: ASPNETCORE_ENVIRONMENT || DOTNET_ENVIRONMENT
    /// Если не определены, то Production
    /// см. https://docs.microsoft.com/ru-ru/aspnet/core/fundamentals/environments?view=aspnetcore-6.0
    /// </summary>
    /// <returns></returns>
    public static string GetDonetCoreEnvironmentVarible()
    {
        return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ??
                          Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ??
                          "Production";
    }
}