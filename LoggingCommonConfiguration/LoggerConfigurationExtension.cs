using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace LoggingCommonConfiguration;

public static class LoggerConfigurationExtension
{
    public static List<Uri> SerilogConnectionStrings(this IConfiguration configuration)
    {
        var res = new List<Uri>();

        foreach (var t in configuration.AsEnumerable())
        {
            var key = t.Key;
            var urlStr = configuration[key];
            if (Regex.IsMatch(key, "Serilog:WriteTo:\\d:Args:nodeUris") && !string.IsNullOrEmpty(urlStr))
            {
                res.Add(new Uri(urlStr));
            }
        }

        return res;
    }
}

