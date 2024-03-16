using AsyncCourse.Issues.Api.Models.Accounts;
using AsyncCourse.Template.Kafka.MessageBus.Models.Accounts;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events;

namespace AsyncCourse.Issues.Daemon;

public static class AccountMapper
{
    public static IssueAccount MapAccount(MessageBusAccount messageBusAccount)
    {
        return new IssueAccount
        {
            AccountId = messageBusAccount.AccountId,
            Email = messageBusAccount.Email,
            Name = messageBusAccount.Name,
            Surname = messageBusAccount.Surname,
            Role = Map(messageBusAccount.Role)
        };

        IssueAccountRole Map(string role)
        {
            return role switch
            {
                "Employee" => IssueAccountRole.Employee,
                "Administrator" => IssueAccountRole.Administrator,
                "Manager" => IssueAccountRole.Manager,
                "Accountant" => IssueAccountRole.Accountant,
                _ => IssueAccountRole.Unknown
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
    
    public static MessageBusAccountsEventType GetBusinessType(string type)
    {
        return type switch
        {
            "RoleChanged" => MessageBusAccountsEventType.RoleChanged,
            _ => MessageBusAccountsEventType.Unknown
        };
    }
}