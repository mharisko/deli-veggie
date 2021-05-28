
namespace DeliVeggie.Product.Service
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using DeliVeggie.Product.Service.Abstract.MessageBus;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
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
                var services = Startup.ConfigureServices(config);
                var serviceProvider = services.BuildServiceProvider();

                await Task.WhenAll(
                    serviceProvider.GetService<IPriceReductionMessageBus>().StartAsync(cancellationTokenSource.Token),
                    serviceProvider.GetService<IProductMessageBus>().StartAsync(cancellationTokenSource.Token)
                    );

                Console.Read();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                cancellationTokenSource.Cancel(true);
                NLog.LogManager.Shutdown();
            }
        }
    }
}
