
namespace DeliVeggie.Product.Service
{
    using DeliVeggie.Product.Service.Abstract.Domain;
    using DeliVeggie.Product.Service.Abstract.MessageBus;
    using DeliVeggie.Product.Service.Abstract.Repository;
    using DeliVeggie.Product.Service.Domain;
    using DeliVeggie.Product.Service.Mongo.Repository;
    using EasyNetQ;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns></returns>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddLogging();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IPriceReductionService, PriceReductionService>();

            services.AddSingleton<IPriceReductionMessageBusService, PriceReductionMessageBusService>();
            services.AddSingleton<IProductMessageBusService, ProductMessageBusService>();
            services.AddHostedService<ProductMessageBusService>();
            services.AddHostedService<PriceReductionMessageBusService>();

            var rabbitMqConnection = configuration.GetConnectionString("RabbitMqConnectionString");
            services.AddSingleton((service) => RabbitHutch.CreateBus(rabbitMqConnection));

            var mongoConnection = configuration.GetConnectionString("MongoConnectionString");
            services.AddSingleton<IProductRepository>((service) =>
            {
                return new ProductRepository(mongoConnection, "deli-veggie-products");
            });

            services.AddSingleton<IPriceReductionRepository>((service) =>
            {
                return new PriceReductionRepository(mongoConnection, "deli-veggie-week-price");
            });
        }
    }
}
