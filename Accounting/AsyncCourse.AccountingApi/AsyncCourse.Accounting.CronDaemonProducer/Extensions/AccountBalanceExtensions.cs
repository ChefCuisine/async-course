using System.Globalization;
using AsyncCourse.Accounting.Api.Models.Balances;
using AsyncCourse.Template.Kafka.MessageBus;
using AsyncCourse.Template.Kafka.MessageBus.Models.AccountBalances;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events.AccountBalances;
using Newtonsoft.Json;

namespace AsyncCourse.Accounting.CronDaemonProducer.Extensions;

public static class AccountBalanceExtensions
{
    // CUD-events
    
    public static MessageBusAccountBalanceStreamEvent GetEventCreated(this AccountBalance accountBalance)
    {
        return new MessageBusAccountBalanceStreamEvent
        {
            MetaInfo = GetForStreamEvent(MessageBusStreamEventType.Created),
            Context = Map(accountBalance)
        };
    }
    
    public static string ToStreamMessage(this MessageBusAccountBalanceStreamEvent streamEvent)
    {
        return JsonConvert.SerializeObject(streamEvent);
    }
    
    // Business events
    
    public static MessageBusAccountBalanceEvent GetEventNewDayBalanceCreated(this AccountBalance accountBalance)
    {
        return new MessageBusAccountBalanceEvent
        {
            MetaInfo = GetForBusinessEvent(MessageBusAccountBalanceEventType.NextDayBalanceCreated),
            Context = Map(accountBalance)
        };
    }
    
    public static string ToBusinessMessage(this MessageBusAccountBalanceEvent businessEvent)
    {
        return JsonConvert.SerializeObject(businessEvent);
    }
    
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
            ProducerName = Constants.Producers.AccountBalanceStream
        };
    }
    
    private static MessageBusEventMetaInfo GetForBusinessEvent(
        MessageBusAccountBalanceEventType eventType,
        int? version = null)
    {
        return new MessageBusEventMetaInfo
        {
            EventId = Guid.NewGuid().ToString(),
            EventType = eventType.ToString(),
            EventVersion = version ?? 1,
            EventDateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
            ProducerName = Constants.Producers.AccountBalanceBusiness
        };
    }
    
    // Mapping
    
    private static MessageBusAccountBalance Map(AccountBalance accountBalance)
    {
        return new MessageBusAccountBalance
        {
            AccountId = accountBalance.AccountId,
            Total = accountBalance.Total.Value,
            Date = accountBalance.Date
        };
    }
}