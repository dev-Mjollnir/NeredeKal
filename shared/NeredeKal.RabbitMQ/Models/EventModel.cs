using NeredeKal.Infrastructure.Enums;

namespace NeredeKal.RabbitMQ.Models
{
    public class EventModel
    {
        public Guid Id { get; set; }
        public EventType EventType { get; set; }
    }
}
