namespace CashFlow.Sidecar.MQ.Base.Interfaces
{
    public interface IQueueConfiguration
    {
        public string HostName { get; set; }
        public string QueueName { get; set; }
        public string RoutingKey { get; set; }
        public string ExchangeName { get; set; }
    }
}
