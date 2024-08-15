using CashFlow.Reports.API.Infrastructure.MQ.Queues.Interfaces;
using CashFlow.Sidecar.MQ.Base.Configurations;

namespace CashFlow.Reports.API.Infrastructure.MQ.Queues
{
    public class CashOperationQueue : QueueConfiguration, ICashOperationQueue
    {
    }
}
