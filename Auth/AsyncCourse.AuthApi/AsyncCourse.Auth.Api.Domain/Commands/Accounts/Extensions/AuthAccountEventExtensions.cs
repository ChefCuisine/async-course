using System.Globalization;
using AsyncCourse.Auth.Api.Models.Accounts;
using AsyncCourse.Template.Kafka.MessageBus;
using AsyncCourse.Template.Kafka.MessageBus.Models.Accounts;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events.Accounts;
using Newtonsoft.Json;

namespace AsyncCourse.Auth.Api.Domain.Commands.Accounts.Extensions;

public static class AuthAccountEventExtensions
{
    // CUD-events

    public static MessageBusAccountStreamEvent GetEventCreated(this AuthAccount account)
    {
        return new MessageBusAccountStreamEvent
        {
            MetaInfo = GetForStreamEvent(MessageBusStreamEventType.Created),
            Context = Map(account)
        };
    }

    public static MessageBusAccountStreamEvent GetEventUpdated(this AuthAccount account)
    {
        return new MessageBusAccountStreamEvent
        {
            MetaInfo = GetForStreamEvent(MessageBusStreamEventType.Updated),
            Context = Map(account)
        };
    }

    public static MessageBusAccountStreamEvent GetEventDeleted(this AuthAccount account)
    {
        return new MessageBusAccountStreamEvent
        {
            MetaInfo = GetForStreamEvent(MessageBusStreamEventType.Deleted),
            Context = Map(account)
        };
    }
    
    public static string ToStreamMessage(this MessageBusAccountStreamEvent streamEvent)
    {
        return JsonConvert.SerializeObject(streamEvent);
    }
    
    // Business events
    
    public static MessageBusAccountsEvent GetEventRoleChanged(this AuthAccount account)
    {
        return new MessageBusAccountsEvent
        {
            MetaInfo = GetForBusinessEvent(MessageBusAccountsEventType.RoleChanged),
            Context = Map(account)
        };
    }
    
    public static string ToBusinessMessage(this MessageBusAccountsEvent businessEvent)
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
            ProducerName = Constants.Producers.AccountsStream
        };
    }
    
    private static MessageBusEventMetaInfo GetForBusinessEvent(
        MessageBusAccountsEventType eventType,
        int? version = null)
    {
        return new MessageBusEventMetaInfo
        {
            EventId = Guid.NewGuid().ToString(),
            EventType = eventType.ToString(),
            EventVersion = version ?? 1,
            EventDateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
            ProducerName = Constants.Producers.AccountsBusiness
        };
    }
    
    // Mapping
    
    private static MessageBusAccount Map(AuthAccount account)
    {
        return new MessageBusAccount
        {
            AccountId = account.Id,
            Email = account.Email,
            Name = account.Name,
            Surname = account.Surname,
            Role = account.Role.ToString()
        };
    }
}