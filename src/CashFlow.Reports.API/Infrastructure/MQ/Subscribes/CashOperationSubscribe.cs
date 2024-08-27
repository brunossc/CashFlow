using CashFlow.Financial.API.Domain.Services.Interfaces;
using CashFlow.Reports.API.Domain.Entities;
using CashFlow.Reports.API.Infrastructure.MQ.Queues.Interfaces;
using CashFlow.Sidecar.MQ.Base;

namespace CashFlow.Reports.API.Infrastructure.MQ.Subscribes
{
    public class CashOperationSubscribe : SubscriberService
    {
        private readonly ICashOperationService _service;
        public CashOperationSubscribe(ICashOperationQueue config, IServiceScopeFactory factory) : base(config)
        {
            _service = factory.CreateScope().ServiceProvider.GetRequiredService<ICashOperationService>();
        }

        protected override Task ProcessMessageAsync(string message, CancellationToken stoppingToken)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                var operation = Newtonsoft.Json.JsonConvert.DeserializeObject<CashOperation>(message);

                if (operation != null)
                {
                    _service.SetOperation(operation);
                }
            }

            return Task.CompletedTask;
        }
    }
}
