using CashFlow.Reports.API.Infrastructure.Database.Repositories;
using CashFlow.Reports.API.Infrastructure.Database.Repositories.Interfaces;
using CashFlow.Reports.API.Infrastructure.MQ.Queues;
using CashFlow.Reports.API.Infrastructure.MQ.Queues.Interfaces;
using CashFlow.Reports.API.Infrastructure.MQ.Subscribes;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CashFlow.Payment.API.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static void AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            AddMQSubscribes(services, configuration);
            AddDatabaseServices(services, configuration);
            AddRepositories(services);
        }

        private static void AddMQSubscribes(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CashOperationQueue>(options => configuration.GetSection("MQConfiguration:CashOperationQueue").Bind(options));
            services.AddSingleton<ICashOperationQueue, CashOperationQueue>(provider => provider.GetRequiredService<IOptions<CashOperationQueue>>().Value);
            services.AddHostedService<CashOperationSubscribe>();
        }

        private static void AddDatabaseServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoClient>(mc =>
            {
                var settings = MongoClientSettings.FromConnectionString(configuration.GetConnectionString("NoSQLConnection"));
                settings.MaxConnectionPoolSize = 100;
                return new MongoClient(settings);
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ICashOperationRepository, CashOperationRepository>();
        }
    }
}
