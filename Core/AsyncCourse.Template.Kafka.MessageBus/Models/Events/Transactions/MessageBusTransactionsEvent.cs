using AsyncCourse.Template.Kafka.MessageBus.Models.Transactions;

namespace AsyncCourse.Template.Kafka.MessageBus.Models.Events.Transactions;

public class MessageBusTransactionsEvent : MessageBusEvent
{
    public MessageBusTransaction Context { get; set; }
}