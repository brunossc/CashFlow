using CashFlow.Payment.API.Infrastructure.MQ.Queue.Interfaces;
using CashFlow.Sidecar.MQ.Base;

namespace CashFlow.Payment.API.Infrastructure.MQ.Subscribes
{
    public class CashOperationSubscribe : SubscriberService
    {
        public CashOperationSubscribe(ICashOperationQueue config)
        : base(config)
        {
        }

        protected override Task ProcessMessageAsync(string message, CancellationToken stoppingToken)
        {
            Console.WriteLine($"Received message: {message}");
            // Implementar o processamento da mensagem aqui
            return Task.CompletedTask;
        }
    }
}
