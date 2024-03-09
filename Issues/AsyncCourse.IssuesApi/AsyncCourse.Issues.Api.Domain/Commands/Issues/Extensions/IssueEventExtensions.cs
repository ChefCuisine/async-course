using AsyncCourse.Issues.Api.Models.Issues;
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
            Type = MessageBusIssueStreamEventType.Created,
            Context = Map(issue)
        };
    }
    
    public static MessageBusIssuesStreamEvent GetEventDeleted(this Issue issue)
    {
        return new MessageBusIssuesStreamEvent
        {
            Type = MessageBusIssueStreamEventType.Deleted,
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
        // todo когда посылается событие "Задача выполнена", то в приницпе
        // необязательно отправлять всю задачу как контекст, можно только ключевые айдишники для последующей обработки
        // условно говоря - Status в отправляемом контексте не нужен, потому что есть EventType
        
        return new MessageBusIssuesEvent
        {
            Type = MessageBusIssuesEventType.IssueDone,
            Context = Map(issue)
        };
    }
    
    public static string ToBusinessMessage(this MessageBusIssuesEvent businessEvent)
    {
        return JsonConvert.SerializeObject(businessEvent);
    }
    
    public static MessageBusIssuesEvent GetEventIssueReassigned(this Issue issue)
    {
        return new MessageBusIssuesEvent
        {
            Type = MessageBusIssuesEventType.IssueReassigned,
            Context = Map(issue)
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
}