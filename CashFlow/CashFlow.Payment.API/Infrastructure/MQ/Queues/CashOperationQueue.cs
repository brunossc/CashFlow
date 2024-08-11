using CashFlow.Payment.API.Infrastructure.MQ.Queue.Interfaces;
using CashFlow.Sidecar.MQ.Base.Configurations;

namespace CashFlow.Payment.API.Infrastructure.MQ.Queues
{
    public class CashOperationQueue : QueueConfiguration, ICashOperationQueue
    {
    }
}
