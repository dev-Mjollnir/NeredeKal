using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NeredeKal.RabbitMQ.Models;
using RabbitMQ.Client;
using System.Text;

namespace NeredeKal.RabbitMQ.Services
{
    public class RabbitMQClientService
    {
        private readonly RabbitMQSettings _settings;
        private readonly ILogger<RabbitMQClientService> _logger;

        public RabbitMQClientService(IOptions<RabbitMQSettings> options, ILogger<RabbitMQClientService> logger)
        {
            _settings = options.Value;
            _logger = logger;
        }

        public async void PublishMessage(string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _settings.Host,
                Port = _settings.Port,
                UserName = _settings.UserName,
                Password = _settings.Password
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: _settings.QueueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);
            var props = new BasicProperties();
            props.ContentType = "application/json";
            props.DeliveryMode = DeliveryModes.Persistent;
            await channel.BasicPublishAsync(exchange: "",
                                 routingKey: _settings.QueueName,
                                 basicProperties: props,
                                 mandatory: true,
                                 body: body);

            _logger.LogInformation($"[RabbitMQ] Sent: {message}");
        }
    }
}
