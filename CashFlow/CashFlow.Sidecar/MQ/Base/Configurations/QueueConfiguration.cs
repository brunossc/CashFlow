using CashFlow.Sidecar.MQ.Base.Interfaces;

namespace CashFlow.Sidecar.MQ.Base.Configurations
{
    public class QueueConfiguration : IQueueConfiguration
    {
        public string HostName { get; set; }
        public string QueueName { get; set; }
        public string RoutingKey { get; set; }
        public string ExchangeName { get; set; }
    }
}
