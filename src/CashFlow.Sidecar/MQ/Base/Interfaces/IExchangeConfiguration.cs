namespace CashFlow.Sidecar.MQ.Base.Interfaces
{
    public interface IExchangeConfiguration
    {
        public string HostName { get; set; }
        public string ExchangeName { get; set; }
        public string RoutingKey { get; set; }
    }
}