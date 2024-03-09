using AsyncCourse.Issues.Api.Models.Accounts;
using AsyncCourse.Template.Kafka.MessageBus.Models.Accounts;

namespace AsyncCourse.Issues.Daemon;

public static class IssuesMapper
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
}