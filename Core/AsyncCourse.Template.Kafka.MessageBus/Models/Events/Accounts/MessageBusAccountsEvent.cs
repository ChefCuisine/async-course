using AsyncCourse.Template.Kafka.MessageBus.Models.Accounts;

namespace AsyncCourse.Template.Kafka.MessageBus.Models.Events.Accounts;

public class MessageBusAccountsEvent
{
    public MessageBusAccountsEventType Type { get; set; }
    public MessageBusAccount Context { get; set; }
}