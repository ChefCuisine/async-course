using AsyncCourse.Accounting.Api.Client;
using AsyncCourse.Template.Kafka.MessageBus;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events.Issues;
using AsyncCourse.Template.Kafka.MessageBus.Models.Issues;
using Vostok.Logging.Abstractions;

namespace AsyncCourse.Accounting.IssuesDaemon;

public class IssuesHanler
{
    private readonly ITemlateKafkaMessageBus kafkaBus;
    private readonly IAccountingApiClient accountingApiClient;

    public IssuesHanler()
    {
        kafkaBus = new TemlateKafkaMessageBus();
        accountingApiClient = new AccountingApiClient(AccountingApiLocalAddress.Get(), new CompositeLog());
    }

    public async Task ProcessStreamEvent(CancellationToken cancellationToken)
    {
        var streamResult = ConsumeEvent<MessageBusIssuesStreamEvent>(Constants.IssuesStreamTopic, cancellationToken);
        if (streamResult == null)
        {
            // todo add log
            return;
        }

        var metaInfo = streamResult.MetaInfo;
        var issue = IssueMapper.MapIssue(streamResult.Context);

        switch (IssueMapper.GetStreamType(metaInfo.EventType))
        {
            case MessageBusStreamEventType.Created:
                await accountingApiClient.SaveIssueAsync(issue);
                break;
            case MessageBusStreamEventType.Updated:
                // todo
                break;
            case MessageBusStreamEventType.Deleted:
                // todo
                break;
            case MessageBusStreamEventType.Unknown:
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public async Task ProcessBusinessEvent(CancellationToken cancellationToken)
    {
        var businessResult = ConsumeEvent<MessageBusIssuesEvent>(Constants.IssuesTopic, cancellationToken);
        if (businessResult == null)
        {
            // todo add log
            return;
        }

        var metaInfo = businessResult.MetaInfo;
        var changedIssue = IssueMapper.MapBusinessChangedIssue(businessResult.Context);

        switch (IssueMapper.GetBusinessType(metaInfo.EventType))
        {
            case MessageBusIssuesEventType.IssueDone:
                await accountingApiClient.CloseIssueAsync(changedIssue);
                break;
            case MessageBusIssuesEventType.IssueReassigned:
                await accountingApiClient.ReassignIssueAsync(changedIssue);
                break;
            case MessageBusIssuesEventType.Unknown:
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