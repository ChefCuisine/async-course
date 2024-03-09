using AsyncCourse.Accounting.Api.Models.Accounts;
using AsyncCourse.Template.Kafka.MessageBus.Models.Accounts;

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

    public static MessageBusAccountStreamEventType GetStreamType(string type)
    {
        return type switch
        {
            "Created" => MessageBusAccountStreamEventType.Created,
            "Updated" => MessageBusAccountStreamEventType.Updated,
            "Deleted" => MessageBusAccountStreamEventType.Deleted,
            _ => MessageBusAccountStreamEventType.Unknown
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