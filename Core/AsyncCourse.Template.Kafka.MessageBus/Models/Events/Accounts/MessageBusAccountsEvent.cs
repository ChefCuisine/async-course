using AsyncCourse.Template.Kafka.MessageBus.Models.Accounts;

namespace AsyncCourse.Template.Kafka.MessageBus.Models.Events.Accounts;

public class MessageBusAccountsEvent : MessageBusEvent
{
    public MessageBusAccount Context { get; set; }
}