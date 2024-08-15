using CashFlow.Financial.API.Infrastructure.MQ.Exchanges.Interfaces;
using CashFlow.Financial.API.Infrastructure.MQ.Publishers.Interfaces;
using CashFlow.Sidecar.MQ.Base;
using CashFlow.Sidecar.MQ.Base.Interfaces;
using System.Runtime.CompilerServices;

namespace CashFlow.Financial.API.Infrastructure.MQ.Publishers
{
    public class CashOperationReportPublisher : PublisherService, ICashOperationReportPublisher
    {
        private readonly IExchangeConfiguration _config;
        public CashOperationReportPublisher(ICashOperationExchange configuration) : base()
        {
            _config = configuration;
        }

        public void EnsureCreated()
        {
            base.EnsureCreated(_config);
        }

        public void SendMessage(string message)
        {
            Publish(message);
        }
    }
}
