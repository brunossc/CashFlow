using CashFlow.Payment.API.Infrastructure.MQ.Exchanges.Interfaces;
using CashFlow.Payment.API.Infrastructure.MQ.Publishers.Interfaces;
using CashFlow.Sidecar.MQ.Base;

namespace CashFlow.Payment.API.Infrastructure.MQ.Publishers
{
    public class CashOperationReportPublisher : PublisherService, ICashOperationReportPublisher
    {
        public CashOperationReportPublisher(ICashOperationExchange configuration)
      : base(configuration)
        {
        }

        public void SendMessage(string message)
        {
            Publish(message);
        }
    }
}
