using AsyncCourse.Accounting.Api.Models.Transactions;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events;
using AsyncCourse.Template.Kafka.MessageBus.Models.Transactions;

namespace AsyncCourse.Accounting.TransactionsDaemonConsumer;

public static class TransactionMapper
{
    public static Transaction MapTransaction(MessageBusTransaction messageBusTransaction)
    {
        return new Transaction
        {
            Id = messageBusTransaction.TransactionId,
        };
    }
    
    public static MessageBusStreamEventType GetStreamType(string type)
    {
        return type switch
        {
            "Created" => MessageBusStreamEventType.Created,
            "Updated" => MessageBusStreamEventType.Updated,
            "Deleted" => MessageBusStreamEventType.Deleted,
            _ => MessageBusStreamEventType.Unknown
        };
    }
    
    public static MessageBusTransactionEventType GetBusinessType(string type)
    {
        return type switch
        {
            // "RoleChanged" => MessageBusTransactionEventType.RoleChanged,
            _ => MessageBusTransactionEventType.Unknown
        };
    }
}