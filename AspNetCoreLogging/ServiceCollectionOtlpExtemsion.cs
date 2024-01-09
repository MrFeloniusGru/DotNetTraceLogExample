using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using B3Propagator = OpenTelemetry.Extensions.Propagators.B3Propagator;
using ILogger = Serilog.ILogger;

namespace DotnetLogging.AspNetCoreSerilogExtension
{
    public static class ServiceCollectionOtlpExtemsion
    {
        /// <summary>
        /// Добовляет телеметрию на входящие и исходящие http запросы. отправляет телеметрию в OTLP
        /// </summary>
        /// <param name="services"></param>
        /// <param name="appName">имя, которое будет добавлено в телеметрию в качестве имени сервиса</param>
        /// <param name="exludedUris">список regex которые стоит исключить из исходящей телеметрии</param>
        /// <returns></returns>
        public static IServiceCollection AddOpenTelemetry(this IServiceCollection services, Uri? optlUri, string? appName, List<string> excludeUriPatterns, ILogger logger)
        {
            //Special case when using insecure channel in the dotnetcore3.1
#if NETCOREAPP3_1
            if(string.Equals("http",optlUri.Scheme, StringComparison.OrdinalIgnoreCase))
            {
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            }
#endif

            Sdk.SetDefaultTextMapPropagator(new B3Propagator());

            services.AddOpenTelemetry()
                .WithTracing(builder =>
                {
                    if (!string.IsNullOrEmpty(appName))
                    {
                        builder.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(appName));
                    }
                    builder
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation(o =>
                        {
                            o.FilterHttpRequestMessage = requestMessage =>
                            {
                                return excludeUriPatterns.All(x =>
                                    !Regex.IsMatch(requestMessage.RequestUri?.OriginalString ?? string.Empty, x));
                            };
                        })
                        .AddConsoleExporter();

                    if(optlUri != null)
                    {
                        logger.Debug($"Add otlp exporter {optlUri}");
                        builder.AddOtlpExporter(otlpOptions => { 
                            otlpOptions.Endpoint = optlUri;
                            otlpOptions.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                        });
                    }    
                });

            return services;
        }
    }
}
