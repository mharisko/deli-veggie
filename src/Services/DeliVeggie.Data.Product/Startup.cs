
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
        public static IServiceCollection ConfigureServices(IConfigurationRoot config)
        {
            var serviceCollection = new ServiceCollection();
            RegisterRepository(serviceCollection, config);

            serviceCollection.AddTransient<IProductService, ProductService>();
            serviceCollection.AddTransient<IPriceReductionService, PriceReductionService>();

            serviceCollection.AddSingleton<IPriceReductionMessageBus, PriceReductionMessageBus>();
            serviceCollection.AddSingleton<IProductMessageBus, ProductMessageBus>();

            var rabbitMqConnection = config.GetConnectionString("RabbitMqConnection");
            serviceCollection.AddSingleton((service) => RabbitHutch.CreateBus(rabbitMqConnection));

            return serviceCollection;
        }

        private static void RegisterRepository(ServiceCollection servicesCollection, IConfiguration configuration)
        {
            var mongoConnection = configuration.GetConnectionString("MongoConnectionString");
            servicesCollection.AddSingleton<IProductRepository>((service) =>
            {
                return new ProductRepository(mongoConnection, "deli-veggie-products");
            });

            servicesCollection.AddSingleton<IPriceReductionRepository>((service) =>
            {
                return new PriceReductionRepository(mongoConnection, "deli-veggie-week-price");
            });
        }
    }
}
