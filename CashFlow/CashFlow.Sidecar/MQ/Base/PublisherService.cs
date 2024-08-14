using CashFlow.Sidecar.MQ.Base.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System.Text;

namespace CashFlow.Sidecar.MQ.Base
{
    public abstract class PublisherService : IDisposable
    {
        private IConnection _connection;
        private IModel _channel;
        private string _exchangeName;
        private string _routingKey;

        protected PublisherService()
        { }

        protected void EnsureCreated(IExchangeConfiguration configuration)
        {
            _exchangeName = configuration.ExchangeName;
            _routingKey = configuration.RoutingKey;

            var factory = new ConnectionFactory() { HostName = configuration.HostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Direct, durable: true);
        }

        protected void Publish(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: _exchangeName,
                                 routingKey: _routingKey,
                                 basicProperties: null,
                                 body: body);

        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}
