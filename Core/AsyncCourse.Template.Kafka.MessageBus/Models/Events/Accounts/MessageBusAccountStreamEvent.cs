using AsyncCourse.Template.Kafka.MessageBus.Models.Accounts;

namespace AsyncCourse.Template.Kafka.MessageBus.Models.Events.Accounts;

public class MessageBusAccountStreamEvent : MessageBusEvent
{
    public MessageBusAccount Context { get; set; }
}