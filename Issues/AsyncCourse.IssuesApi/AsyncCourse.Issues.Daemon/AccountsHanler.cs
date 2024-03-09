using AsyncCourse.Issues.Api.Client;
using AsyncCourse.Template.Kafka.MessageBus;
using AsyncCourse.Template.Kafka.MessageBus.Models.Accounts;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events.Accounts;
using Vostok.Logging.Abstractions;

namespace AsyncCourse.Issues.Daemon;

public class AccountsHanler
{
    private readonly ITemlateKafkaMessageBus kafkaBus;
    private readonly IIssuesApiClient issuesApiClient;

    public AccountsHanler()
    {
        kafkaBus = new TemlateKafkaMessageBus();
        issuesApiClient = new IssuesApiClient(IssuesApiLocalAddress.Get(), new CompositeLog());
    }

    public async Task ProcessStreamEvent(CancellationToken cancellationToken)
    {
        var streamResult = ConsumeEvent<MessageBusAccountStreamEvent>(Constants.AccountsStreamTopic, cancellationToken);
        if (streamResult == null)
        {
            // todo add log
            return;
        }

        var metaInfo = streamResult.MetaInfo;
        switch (AccountMapper.GetStreamType(metaInfo.EventType))
        {
            case MessageBusAccountStreamEventType.Created:
                var account = AccountMapper.MapAccount(streamResult.Context);
                await issuesApiClient.SaveAccountAsync(account);
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

    public async Task ProcessBusinessEvent(CancellationToken cancellationToken)
    {
        var businessResult = ConsumeEvent<MessageBusAccountsEvent>(Constants.AccountsTopic, cancellationToken);
        if (businessResult == null)
        {
            // todo add log
            return;
        }

        var metaInfo = businessResult.MetaInfo;
        switch (AccountMapper.GetBusinessType(metaInfo.EventType))
        {
            case MessageBusAccountsEventType.RoleChanged:
                var account = AccountMapper.MapAccount(businessResult.Context);
                await issuesApiClient.UpdateAccountAsync(account);
                break;
            case MessageBusAccountsEventType.Unknown:
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private TEvent ConsumeEvent<TEvent>(string topic, CancellationToken cancellationToken) where TEvent : MessageBusEvent
    {
        var streamResult = kafkaBus.Consume<TEvent>(topic, cancellationToken);
        if (streamResult == default)
        {
            return null;
        }

        var metaInfo = streamResult.MetaInfo;
        if (metaInfo == default)
        {
            return null;
        }

        return streamResult;
    }
}