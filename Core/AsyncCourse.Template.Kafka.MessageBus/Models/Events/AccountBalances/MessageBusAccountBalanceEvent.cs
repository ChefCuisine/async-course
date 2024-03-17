using AsyncCourse.Template.Kafka.MessageBus.Models.AccountBalances;

namespace AsyncCourse.Template.Kafka.MessageBus.Models.Events.AccountBalances;

public class MessageBusAccountBalanceEvent : MessageBusEvent
{
    public MessageBusAccountBalance Context { get; set; }
}