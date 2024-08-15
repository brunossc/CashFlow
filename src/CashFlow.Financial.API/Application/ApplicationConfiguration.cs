using CashFlow.Financial.API.Domain.Services;
using CashFlow.Financial.API.Domain.Services.Interfaces;

namespace CashFlow.Financial.API.Application
{
    public static class ApplicationConfiguration
    {
        public static void AddApplicationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            AddServices(services);
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddTransient<ICashOperationService, CashOperationService>();
        }
    }
}
