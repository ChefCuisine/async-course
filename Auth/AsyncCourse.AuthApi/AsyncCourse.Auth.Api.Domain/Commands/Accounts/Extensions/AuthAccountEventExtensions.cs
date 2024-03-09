using AsyncCourse.Auth.Api.Models.Accounts;
using AsyncCourse.Template.Kafka.MessageBus.Models.Accounts;
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
            Type = MessageBusAccountStreamEventType.Created,
            Context = Map(account)
        };
    }

    public static MessageBusAccountStreamEvent GetEventUpdated(this AuthAccount account)
    {
        return new MessageBusAccountStreamEvent
        {
            Type = MessageBusAccountStreamEventType.Updated,
            Context = Map(account)
        };
    }

    public static MessageBusAccountStreamEvent GetEventDeleted(this AuthAccount account)
    {
        return new MessageBusAccountStreamEvent
        {
            Type = MessageBusAccountStreamEventType.Deleted,
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
            Type = MessageBusAccountsEventType.RoleChanged,
            Context = Map(account)
        };
    }
    
    public static string ToBusinessMessage(this MessageBusAccountsEvent businessEvent)
    {
        return JsonConvert.SerializeObject(businessEvent);
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