using CashFlow.Financial.API.Domain.Services;
using CashFlow.Financial.API.Domain.Services.Interfaces;

namespace CashFlow.Reports.API.Domain
{
    public static class DomainConfiguration
    {
        public static void AddDomainConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            AddServices(services);
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<ICashOperationService, CashOperationService>();
        }
    }
}
