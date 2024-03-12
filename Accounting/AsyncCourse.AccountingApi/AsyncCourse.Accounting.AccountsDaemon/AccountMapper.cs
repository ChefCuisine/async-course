using AsyncCourse.Accounting.Api.Models.Accounts;
using AsyncCourse.Template.Kafka.MessageBus.Models.Accounts;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events;

namespace AsyncCourse.Accounting.AccountsDaemon;

public static class AccountMapper
{
    public static AccountingAccount MapAccount(MessageBusAccount messageBusAccount)
    {
        return new AccountingAccount
        {
            AccountId = messageBusAccount.AccountId,
            Email = messageBusAccount.Email,
            Name = messageBusAccount.Name,
            Surname = messageBusAccount.Surname,
            Role = Map(messageBusAccount.Role)
        };

        AccountingAccountRole Map(string role)
        {
            return role switch
            {
                "Employee" => AccountingAccountRole.Employee,
                "Administrator" => AccountingAccountRole.Administrator,
                "Manager" => AccountingAccountRole.Manager,
                "Accountant" => AccountingAccountRole.Accountant,
                _ => AccountingAccountRole.Unknown
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