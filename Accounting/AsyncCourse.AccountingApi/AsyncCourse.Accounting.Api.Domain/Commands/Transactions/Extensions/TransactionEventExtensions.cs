using System.Globalization;
using AsyncCourse.Accounting.Api.Models.Transactions;
using AsyncCourse.Template.Kafka.MessageBus;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events.Transactions;
using AsyncCourse.Template.Kafka.MessageBus.Models.Transactions;
using Newtonsoft.Json;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Transactions.Extensions;

public static class TransactionEventExtensions
{
    // CUD-events
    
    public static MessageBusTransactionsStreamEvent GetEventCreated(this Transaction transaction)
    {
        return new MessageBusTransactionsStreamEvent
        {
            MetaInfo = GetForStreamEvent(MessageBusTransactionStreamEventType.Created),
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
        MessageBusTransactionStreamEventType eventType,
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

    private static MessageBusTransaction Map(Transaction transaction)
    {
        return new MessageBusTransaction
        {
            TransactionId = transaction.Id
        };
    }
}