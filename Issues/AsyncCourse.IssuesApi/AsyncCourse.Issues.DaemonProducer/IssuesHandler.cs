using AsyncCourse.Issues.Api.Client;
using AsyncCourse.Issues.Api.Models.OutboxEvents;
using AsyncCourse.Issues.DaemonProducer.Extensions;
using AsyncCourse.Template.Kafka.MessageBus;
using Vostok.Logging.Abstractions;

namespace AsyncCourse.Issues.DaemonProducer;

public class IssuesHandler
{
    private readonly ITemlateKafkaMessageBus kafkaBus;
    private readonly IIssuesApiClient issuesApiClient;
    
    public IssuesHandler()
    {
        kafkaBus = new TemlateKafkaMessageBus();
        issuesApiClient = new IssuesApiClient(IssuesApiLocalAddress.Get(), new CompositeLog());
    }
    
    public async Task ProduceEvent()
    {
        var nextEventResult = await issuesApiClient.ReadIssueEventAsync();
        if (!nextEventResult.IsSuccessful)
        {
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
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task SendEventsAsync(IssueOutboxEvent issueEvent)
    {
        if (issueEvent == null)
        {
            return;
        }
        
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