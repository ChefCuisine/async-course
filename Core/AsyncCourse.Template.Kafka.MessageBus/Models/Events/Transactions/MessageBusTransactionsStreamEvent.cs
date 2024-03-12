using AsyncCourse.Template.Kafka.MessageBus.Models.Transactions;

namespace AsyncCourse.Template.Kafka.MessageBus.Models.Events.Transactions;

public class MessageBusTransactionsStreamEvent : MessageBusEvent
{
    public MessageBusTransaction Context { get; set; }
}