using AsyncCourse.Accounting.Api.Models.Balances;
using AsyncCourse.Template.Kafka.MessageBus.Models.AccountBalances;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events;

namespace AsyncCourse.Accounting.CronDaemonConsumer;

public static class AccountBalanceMapper
{
    public static AccountBalance MapBalance(MessageBusAccountBalance messageBusBalance)
    {
        return new AccountBalance
        {
            Id = default,
            AccountId = messageBusBalance.AccountId,
            Date = messageBusBalance.Date,
            Total = messageBusBalance.Total
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
    
    public static MessageBusAccountBalanceEventType GetBusinessType(string type)
    {
        return type switch
        {
            "NextDayBalanceCreated" => MessageBusAccountBalanceEventType.NextDayBalanceCreated,
            _ => MessageBusAccountBalanceEventType.Unknown
        };
    }
}