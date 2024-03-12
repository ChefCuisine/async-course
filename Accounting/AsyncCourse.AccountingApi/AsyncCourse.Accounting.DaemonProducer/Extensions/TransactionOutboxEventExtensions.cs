using System.Globalization;
using AsyncCourse.Accounting.Api.Models.OutboxEvents;
using AsyncCourse.Template.Kafka.MessageBus;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events.Transactions;
using AsyncCourse.Template.Kafka.MessageBus.Models.Transactions;
using Newtonsoft.Json;

namespace AsyncCourse.Accounting.DaemonProducer.Extensions;

public static class TransactionOutboxEventExtensions
{
    // CUD-events
    
    public static MessageBusTransactionsStreamEvent GetEventCreated(this TransactionOutboxEvent transaction)
    {
        return new MessageBusTransactionsStreamEvent
        {
            MetaInfo = GetForStreamEvent(MessageBusStreamEventType.Created),
            Context = Map(transaction)
        };
    }
    
    public static string ToStreamMessage(this MessageBusTransactionsStreamEvent streamEvent)
    {
        return JsonConvert.SerializeObject(streamEvent);
    }
    
    // Business events
    
    // MetaInfo
    
    private static MessageBusEventMetaInfo GetForStreamEvent(
        MessageBusStreamEventType eventType,
        int? version = null)
    {
        return new MessageBusEventMetaInfo
        {
            EventId = Guid.NewGuid().ToString(),
            EventType = eventType.ToString(),
            EventVersion = version ?? 1,
            EventDateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
            ProducerName = Constants.Producers.TransactionsStream
        };
    }
    
    // Mapping

    private static MessageBusTransaction Map(TransactionOutboxEvent transactionEvent)
    {
        return new MessageBusTransaction
        {
            TransactionId = transactionEvent.TransactionId
        };
    }
}