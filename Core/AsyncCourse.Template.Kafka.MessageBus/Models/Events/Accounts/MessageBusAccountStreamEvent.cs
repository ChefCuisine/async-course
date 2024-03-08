using AsyncCourse.Template.Kafka.MessageBus.Models.Accounts;

namespace AsyncCourse.Template.Kafka.MessageBus.Models.Events.Accounts;

public class MessageBusAccountStreamEvent
{
    public MessageBusAccountStreamEventType Type { get; set; }
    public MessageBusAccount Context { get; set; }
}