using CashFlow.Sidecar.MQ.Base.Interfaces;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CashFlow.Sidecar.MQ.Base
{
    public abstract class SubscriberService : BackgroundService
    {
        private readonly string _hostName;
        private readonly string _queueName;
        private readonly string _routingKey;
        private readonly string _exchangeName;
        private bool _binded = false;

        private IConnection _connection;
        private IModel _channel;

        protected SubscriberService(IQueueConfiguration config)
        {
            _hostName = config.HostName;
            _queueName = config.QueueName;
            _routingKey = config.RoutingKey;
            _exchangeName = config.ExchangeName;

            InitializeRabbitMq();
        }

        private void InitializeRabbitMq()
        {
            var factory = new ConnectionFactory() { HostName = _hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _queueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

            if (!string.IsNullOrWhiteSpace(_routingKey) && !string.IsNullOrWhiteSpace(_exchangeName))
            {
                _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Direct, durable: true);
                _channel.QueueBind(
                    queue: _queueName,
                    exchange: _exchangeName,
                    routingKey: _routingKey);
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await ProcessMessageAsync(message, stoppingToken);

                // Acknowledge the message as processed
                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            _channel.BasicConsume(queue: _queueName,
                                  autoAck: false,
                                  consumer: consumer);

            return Task.CompletedTask;
        }

        protected abstract Task ProcessMessageAsync(string message, CancellationToken stoppingToken);

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }
}