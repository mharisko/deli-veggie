﻿
namespace DeliVeggie.Product.Service
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using DeliVeggie.Product.Service.Abstract.MessageBus;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using NLog;
    using NLog.Extensions.Logging;

    class Program
    {
        public static async Task Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var config = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                       .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                       .AddCommandLine(args)
                       .AddEnvironmentVariables().Build();

            var logger = LogManager.Setup()
                                  .SetupExtensions(ext => ext.RegisterConfigSettings(config))
                                  .GetCurrentClassLogger();

            var cancellationTokenSource = new CancellationTokenSource();

            try
            {
                await CreateHostBuilder(args, config).Build().RunAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                cancellationTokenSource.Cancel(true);
                LogManager.Shutdown();
            }
        }

        static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration) =>
           Host.CreateDefaultBuilder(args)
             .ConfigureHostConfiguration(builder => builder.AddConfiguration(configuration))
                    .ConfigureLogging(loggingBuilder =>
                    {
                        loggingBuilder.ClearProviders()
                        .AddNLog()
                        .SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug);
                    })
                    .ConfigureServices((services) =>
                    {
                        Startup.ConfigureServices(services, configuration);
                    });
    }
}
