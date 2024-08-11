using CashFlow.Payment.API.Infrastructure.MQ.Exchange;
using CashFlow.Payment.API.Infrastructure.MQ.Exchanges.Interfaces;
using CashFlow.Payment.API.Infrastructure.MQ.Publishers;
using CashFlow.Payment.API.Infrastructure.MQ.Publishers.Interfaces;
using CashFlow.Payment.API.Infrastructure.MQ.Queue.Interfaces;
using CashFlow.Payment.API.Infrastructure.MQ.Queues;
using CashFlow.Payment.API.Infrastructure.MQ.Subscribes;
using Microsoft.Extensions.Options;

namespace CashFlow.Payment.API.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static void AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            AddMQSubscribes(services, configuration);
            AddMQPublishers(services, configuration);
        }

        private static void AddMQSubscribes(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CashOperationQueue>(options => configuration.GetSection("MQConfiguration:CashOperationQueue").Bind(options));
            services.AddSingleton<ICashOperationQueue, CashOperationQueue>(provider => provider.GetRequiredService<IOptions<CashOperationQueue>>().Value);
            services.AddHostedService<CashOperationSubscribe>();
        }

        private static void AddMQPublishers(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ICashOperationExchange>(options => configuration.GetSection("MQConfiguration:CashOperationExchange").Bind(options));
            services.AddSingleton<ICashOperationExchange, CashOperationExchange>(provider => provider.GetRequiredService<IOptions<CashOperationExchange>>().Value);
            services.AddSingleton<ICashOperationReportPublisher, CashOperationReportPublisher>();

        }
    }
}
