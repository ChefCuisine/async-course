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
            Amount = messageBusTransaction.AnalyticsInfo.Amount,
            Type = Map(messageBusTransaction.AnalyticsInfo.Type),
            CreatedAt = messageBusTransaction.AnalyticsInfo.CreatedAt
        };

        TransactionType Map(string type)
        {
            return type switch
            {
                "RemoveMoney" => TransactionType.IssueAssigned,
                "AddMoney" => TransactionType.IssueDone,
                "DayClosed" => TransactionType.DayClosed,
                _ => TransactionType.Unknown
            };
        }
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
            "AnalyticsSent" => MessageBusTransactionEventType.AnalyticsSent,
            "DayClosed" => MessageBusTransactionEventType.DayClosed,
            _ => MessageBusTransactionEventType.Unknown
        };
    }
}