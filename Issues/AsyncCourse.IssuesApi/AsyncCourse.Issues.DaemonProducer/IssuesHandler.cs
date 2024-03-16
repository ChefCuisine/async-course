using AsyncCourse.Issues.Api.Client;
using AsyncCourse.Issues.Api.Models.OutboxEvents;
using AsyncCourse.Issues.DaemonProducer.Extensions;
using AsyncCourse.Template.Kafka.MessageBus;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Console;

namespace AsyncCourse.Issues.DaemonProducer;

public class IssuesHandler
{
    private readonly ITemlateKafkaMessageBus kafkaBus;
    private readonly IIssuesApiClient issuesApiClient;
    private readonly ILog log;
    
    public IssuesHandler()
    {
        log = new ConsoleLog().WithMinimumLevel(LogLevel.Info);
        kafkaBus = new TemlateKafkaMessageBus();
        issuesApiClient = new IssuesApiClient(IssuesApiLocalAddress.Get(), log);
    }
    
    public async Task ProduceEvent()
    {
        var nextEventResult = await issuesApiClient.ReadIssueEventAsync();
        if (!nextEventResult.IsSuccessful)
        {
            log.Info("...");
            return;
        }

        var nextEvent = nextEventResult.Result;

        try
        {
            await SendEventsAsync(nextEvent);
            await issuesApiClient.DeleteIssueEventAsync(nextEvent.Id);
        }
        catch (Exception e)
        {
            log.Error(e);
            throw;
        }
    }

    private async Task SendEventsAsync(IssueOutboxEvent issueEvent)
    {
        if (issueEvent == null)
        {
            return;
        }
        
        // todo
        // возможно стоит переработать идею:
        // то что задача создалась и сменился ответственный - это и stream- и BE-событие
        // обработав stream-event - мы получим в соответствующей БД нужный слепок Issue (и все)
        // обработав business-event - мы создадим нужную транзакцию (и все)
        switch (issueEvent.Type)
        {
            case IssueOutboxEventType.Created:
            {
                var streamEventMessage = issueEvent
                    .GetEventCreated()
                    .ToStreamMessage();

                await kafkaBus.SendMessageAsync(Constants.IssuesStreamTopic, streamEventMessage);
            }
                break;
            case IssueOutboxEventType.Reassigned:
            {
                var businessEventMessage = issueEvent
                    .GetEventIssueReassigned()
                    .ToBusinessMessage();
        
                await kafkaBus.SendMessageAsync(Constants.IssuesTopic, businessEventMessage);
            }
                break;
            case IssueOutboxEventType.Done:
            {
                var businessEventMessage = issueEvent
                    .GetEventIssueDone()
                    .ToBusinessMessage();
        
                await kafkaBus.SendMessageAsync(Constants.IssuesTopic, businessEventMessage);
            }
                break;
            case IssueOutboxEventType.Unknown:
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}