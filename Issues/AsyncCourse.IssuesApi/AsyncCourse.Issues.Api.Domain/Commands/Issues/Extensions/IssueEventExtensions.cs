using System.Globalization;
using AsyncCourse.Issues.Api.Models.Issues;
using AsyncCourse.Template.Kafka.MessageBus;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events.Issues;
using AsyncCourse.Template.Kafka.MessageBus.Models.Issues;
using Newtonsoft.Json;

namespace AsyncCourse.Issues.Api.Domain.Commands.Issues.Extensions;

public static class IssueEventExtensions
{
    // CUD-events
    
    public static MessageBusIssuesStreamEvent GetEventCreated(this Issue issue)
    {
        return new MessageBusIssuesStreamEvent
        {
            MetaInfo = GetForStreamEvent(MessageBusIssueStreamEventType.Created),
            Context = Map(issue)
        };
    }
    
    public static MessageBusIssuesStreamEvent GetEventDeleted(this Issue issue)
    {
        return new MessageBusIssuesStreamEvent
        {
            MetaInfo = GetForStreamEvent(MessageBusIssueStreamEventType.Deleted),
            Context = Map(issue)
        };
    }

    public static string ToStreamMessage(this MessageBusIssuesStreamEvent streamEvent)
    {
        return JsonConvert.SerializeObject(streamEvent);
    }

    // Business events
    
    public static MessageBusIssuesEvent GetEventIssueDone(this Issue issue)
    {
        return new MessageBusIssuesEvent
        {
            MetaInfo = GetForBusinessEvent(MessageBusIssuesEventType.IssueDone),
            Context = MapToChanged(issue)
        };
    }
    
    public static MessageBusIssuesEvent GetEventIssueReassigned(this Issue issue)
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
        MessageBusIssueStreamEventType eventType,
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
    
    private static MessageBusIssue Map(Issue issue)
    {
        return new MessageBusIssue
        {
            IssueId = issue.Id,
            Title = issue.Title,
            Description = issue.Description,
            Status = issue.Status.ToString(),
            AssignedToAccountId = issue.AssignedToAccountId,
        };
    }
    
    private static MessageBusBusinessChangedIssue MapToChanged(Issue issue)
    {
        return new MessageBusBusinessChangedIssue
        {
            IssueId = issue.Id,
            AssignedToAccountId = issue.AssignedToAccountId
        };
    }
}