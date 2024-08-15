using CashFlow.Sidecar.MQ.Base.Interfaces;

namespace CashFlow.Sidecar.MQ.Base.Configurations
{
    public class ExchangeConfiguration : IExchangeConfiguration
    {
        public string HostName { get; set; }
        public string ExchangeName { get; set; }
        public string RoutingKey { get; set; }
    }
}
