using System.Globalization;
using AsyncCourse.Issues.Api.Models.OutboxEvents;
using AsyncCourse.Template.Kafka.MessageBus;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events.Issues;
using AsyncCourse.Template.Kafka.MessageBus.Models.Issues;
using Newtonsoft.Json;

namespace AsyncCourse.Issues.DaemonProducer.Extensions;

public static class IssueOutboxEventExtensions
{
    // CUD-events
    
    public static MessageBusIssuesStreamEvent GetEventCreated(this IssueOutboxEvent issue)
    {
        return new MessageBusIssuesStreamEvent
        {
            MetaInfo = GetForStreamEvent(MessageBusStreamEventType.Created),
            Context = Map(issue)
        };
    }
    
    public static MessageBusIssuesStreamEvent GetEventDeleted(this IssueOutboxEvent issue)
    {
        return new MessageBusIssuesStreamEvent
        {
            MetaInfo = GetForStreamEvent(MessageBusStreamEventType.Deleted),
            Context = Map(issue)
        };
    }

    public static string ToStreamMessage(this MessageBusIssuesStreamEvent streamEvent)
    {
        return JsonConvert.SerializeObject(streamEvent);
    }

    // Business events
    
    public static MessageBusIssuesEvent GetEventIssueDone(this IssueOutboxEvent issue)
    {
        return new MessageBusIssuesEvent
        {
            MetaInfo = GetForBusinessEvent(MessageBusIssuesEventType.IssueDone),
            Context = MapToChanged(issue)
        };
    }
    
    public static MessageBusIssuesEvent GetEventIssueReassigned(this IssueOutboxEvent issue)
    {
        return new MessageBusIssuesEvent
        {
            MetaInfo = GetForBusinessEvent(MessageBusIssuesEventType.IssueReassigned),
            Context = MapToChanged(issue)
        };
    }
    
    public static string ToBusinessMessage(this MessageBusIssuesEvent businessEvent)
    {
        return JsonConvert.SerializeObject(businessEvent);
    }

    // MetaInfo

    private static MessageBusEventMetaInfo GetForStreamEvent(
        MessageBusStreamEventType eventType,
        int? version = null)
    {
        return new MessageBusEventMetaInfo
        {
            EventId = Guid.NewGuid().ToString(),
            EventType = eventType.ToString(),
            EventVersion = version ?? 1,
            EventDateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
            ProducerName = Constants.Producers.IssuesStream
        };
    }
    
    private static MessageBusEventMetaInfo GetForBusinessEvent(
        MessageBusIssuesEventType eventType,
        int? version = null)
    {
        return new MessageBusEventMetaInfo
        {
            EventId = Guid.NewGuid().ToString(),
            EventType = eventType.ToString(),
            EventVersion = version ?? 1,
            EventDateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
            ProducerName = Constants.Producers.IssuesBusiness
        };
    }
    
    // Mapping
    
    private static MessageBusIssue Map(IssueOutboxEvent issue)
    {
        return new MessageBusIssue
        {
            IssueId = issue.IssueId,
            Title = issue.Title,
            Description = issue.Description,
            Status = issue.IssueStatus,
            AssignedToAccountId = issue.AssignedToAccountId,
        };
    }
    
    private static MessageBusBusinessChangedIssue MapToChanged(IssueOutboxEvent issue)
    {
        return new MessageBusBusinessChangedIssue
        {
            IssueId = issue.IssueId,
            AssignedToAccountId = issue.AssignedToAccountId
        };
    }
}