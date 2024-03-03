using AsyncCourse.Issues.Api.Client;
using AsyncCourse.Issues.Api.Models.Accounts;
using AsyncCourse.Template.Kafka.MessageBus;
using AsyncCourse.Template.Kafka.MessageBus.Models.Accounts;
using Vostok.Logging.Abstractions;

var kafkaBus = new TemlateKafkaMessageBus();
var issuesApiClient = new IssuesApiClient(IssuesApiLocalAddress.Get(), new CompositeLog());

while (true)
{
    var cancellationToken = new CancellationToken();

    var createAccountResult = kafkaBus.Consume<MessageBusAccount>(Constants.AccountCreateTopic, cancellationToken);
    if (createAccountResult != null)
    {
        var account = MapAccount(createAccountResult);
        await issuesApiClient.SaveAsync(account);
    }

    var updateAccountResult = kafkaBus.Consume<MessageBusAccount>(Constants.AccountUpdateTopic, cancellationToken);
    if (updateAccountResult != null)
    {
        await issuesApiClient.SaveAsync(MapAccount(updateAccountResult));
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

