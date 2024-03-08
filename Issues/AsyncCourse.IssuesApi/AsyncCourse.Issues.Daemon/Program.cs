using AsyncCourse.Issues.Api.Client;
using AsyncCourse.Issues.Api.Models.Accounts;
using AsyncCourse.Template.Kafka.MessageBus;
using AsyncCourse.Template.Kafka.MessageBus.Models.Accounts;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events.Accounts;
using Vostok.Logging.Abstractions;

var kafkaBus = new TemlateKafkaMessageBus();
var issuesApiClient = new IssuesApiClient(IssuesApiLocalAddress.Get(), new CompositeLog());

while (true)
{
    var cancellationToken = new CancellationToken();

    var accountsStreamResult = kafkaBus.Consume<MessageBusAccountStreamEvent>(Constants.AccountsStreamTopic, cancellationToken);
    if (accountsStreamResult != null)
    {
        switch (accountsStreamResult.Type)
        {
            case MessageBusAccountStreamEventType.Created:
                var account = MapAccount(accountsStreamResult.Context);
                await issuesApiClient.SaveAsync(account);
                break;
            case MessageBusAccountStreamEventType.Updated:
                // todo
                break;
            case MessageBusAccountStreamEventType.Deleted:
                // todo
                break;
            case MessageBusAccountStreamEventType.Unknown:
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    var accountsResult = kafkaBus.Consume<MessageBusAccountsEvent>(Constants.AccountsTopic, cancellationToken);
    if (accountsResult != null)
    {
        switch (accountsResult.Type)
        {
            case MessageBusAccountsEventType.RoleChanged:
                await issuesApiClient.UpdateAsync(MapAccount(accountsResult.Context));
                break;
            case MessageBusAccountsEventType.Unknown:
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

IssueAccount MapAccount(MessageBusAccount messageBusAccount)
{
    return new IssueAccount
    {
        AccountId = messageBusAccount.Id,
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

