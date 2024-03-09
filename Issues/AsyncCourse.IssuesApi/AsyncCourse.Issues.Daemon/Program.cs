using AsyncCourse.Issues.Api.Client;
using AsyncCourse.Issues.Daemon;
using AsyncCourse.Template.Kafka.MessageBus;
using AsyncCourse.Template.Kafka.MessageBus.Models.Accounts;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events.Accounts;
using Vostok.Logging.Abstractions;

var kafkaBus = new TemlateKafkaMessageBus();
var issuesApiClient = new IssuesApiClient(IssuesApiLocalAddress.Get(), new CompositeLog());

while (true)
{
    var cancellationToken = new CancellationToken();

    await ProcessStreamEvent();
    await ProcessBusinessEvent();

    async Task ProcessStreamEvent()
    {
        var accountsStreamResult = kafkaBus.Consume<MessageBusAccountStreamEvent>(Constants.AccountsStreamTopic, cancellationToken);
        if (accountsStreamResult != null)
        {
            switch (accountsStreamResult.Type)
            {
                case MessageBusAccountStreamEventType.Created:
                    var account = IssuesMapper.MapAccount(accountsStreamResult.Context);
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
    }

    async Task ProcessBusinessEvent()
    {
        var accountsResult = kafkaBus.Consume<MessageBusAccountsEvent>(Constants.AccountsTopic, cancellationToken);
        if (accountsResult != null)
        {
            switch (accountsResult.Type)
            {
                case MessageBusAccountsEventType.RoleChanged:
                    var account = IssuesMapper.MapAccount(accountsResult.Context);
                    await issuesApiClient.UpdateAsync(account);
                    break;
                case MessageBusAccountsEventType.Unknown:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}