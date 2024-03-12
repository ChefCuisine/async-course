using AsyncCourse.Accounting.Api.Models.Issues;
using AsyncCourse.Template.Kafka.MessageBus.Models.Events;
using AsyncCourse.Template.Kafka.MessageBus.Models.Issues;

namespace AsyncCourse.Accounting.IssuesDaemon;

public static class IssueMapper
{
    public static AccountingIssue MapIssue(MessageBusIssue messageBusIssue)
    {
        return new AccountingIssue
        {
            IssueId = messageBusIssue.IssueId,
            Title = messageBusIssue.Title,
            JiraId = messageBusIssue.JiraId,
            Description = messageBusIssue.Description,
            Status = Map(messageBusIssue.Status),
            AssignedToAccountId = messageBusIssue.AssignedToAccountId
        };
    }

    public static AccountingBusinessChangedIssue MapBusinessChangedIssue(MessageBusBusinessChangedIssue changedIssue)
    {
        return new AccountingBusinessChangedIssue
        {
            IssueId = changedIssue.IssueId,
            AssignedToAccountId = changedIssue.AssignedToAccountId
        };
    }

    public static MessageBusStreamEventType GetStreamType(string type)
    {
        return type switch
        {
            "Created" => MessageBusStreamEventType.Created,
            "Updated" => MessageBusStreamEventType.Updated,
            "Deleted" => MessageBusStreamEventType.Deleted,
            _ => MessageBusStreamEventType.Unknown
        };
    }
    
    public static MessageBusIssuesEventType GetBusinessType(string type)
    {
        return type switch
        {
            "IssueDone" => MessageBusIssuesEventType.IssueDone,
            "IssueReassigned" => MessageBusIssuesEventType.IssueReassigned,
            _ => MessageBusIssuesEventType.Unknown
        };
    }
    
    private static AccountingIssueStatus Map(string status)
    {
        return status switch
        {
            "Done" => AccountingIssueStatus.Done,
            "Created" => AccountingIssueStatus.Created,
            _ => AccountingIssueStatus.Unknown
        };
    }
}