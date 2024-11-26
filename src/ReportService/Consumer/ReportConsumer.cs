
using MediatR;
using Microsoft.Extensions.Options;
using NeredeKal.Infrastructure.Enums;
using NeredeKal.RabbitMQ.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportService.Application.Command;
using System.Text;
using System.Text.Json;

namespace ReportService.Consumer
{
    public class ReportConsumer : BackgroundService
    {
        private readonly RabbitMQSettings _settings;
        private readonly ILogger<ReportConsumer> _logger;
        private readonly IMediator _mediator;

        public ReportConsumer(IOptions<RabbitMQSettings> options, ILogger<ReportConsumer> logger, IMediator mediator)
        {
            _settings = options.Value;
            _logger = logger;
            _mediator = mediator;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _settings.Host,
                Port = _settings.Port,
                UserName = _settings.UserName,
                Password = _settings.Password
            };

            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: _settings.QueueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogInformation($"[RabbitMQ] Received: {message}");

                var result = JsonSerializer.Deserialize<EventModel>(message);
                try
                {
                    switch (result?.EventType)
                    {
                        case EventType.HotelReportEvent:
                            await _mediator.Send(new HotelReportCommand { Id = result.Id });
                            break;
                        default:
                            _logger.LogInformation($"[RabbitMQ] Unknown event type");
                            break;
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError($"[RabbitMQ] Error: {ex.Message}");
                }
            };

            await channel.BasicConsumeAsync(queue: _settings.QueueName,
                                 autoAck: true,
                                 consumer: consumer);

            return;
        }
    }
}
