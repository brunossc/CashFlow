using CashFlow.Payment.API.Domain.Services;
using CashFlow.Payment.API.Infrastructure.Database;
using CashFlow.Payment.API.Infrastructure.Database.Repositories;
using CashFlow.Payment.API.Infrastructure.Database.Repositories.Interfaces;
using CashFlow.Payment.API.Infrastructure.MQ.Exchanges;
using CashFlow.Payment.API.Infrastructure.MQ.Exchanges.Interfaces;
using CashFlow.Payment.API.Infrastructure.MQ.Publishers;
using CashFlow.Payment.API.Infrastructure.MQ.Publishers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CashFlow.Payment.API.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static void AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            AddMQPublishers(services, configuration);
            AddDatabaseServices(services, configuration);
            AddRepositories(services);
        }

        private static void AddMQPublishers(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CashOperationExchange>(options => configuration.GetSection("MQConfiguration:CashOperationExchange").Bind(options));
            services.AddSingleton<ICashOperationExchange, CashOperationExchange>(provider => provider.GetRequiredService<IOptions<CashOperationExchange>>().Value);
            services.AddSingleton<ICashOperationReportPublisher, CashOperationReportPublisher>();
        }

        private static void AddDatabaseServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SQLConnection"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 2,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null);
                }
            ), ServiceLifetime.Transient);
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ICashOperationRepository, CashOperationRepository>();
        }
    }
}
