using AsyncCourse.Issues.Api.Models.Issues;
using AsyncCourse.Issues.Api.Models.OutboxEvents;

namespace AsyncCourse.Issues.Api.Domain.Commands.Issues;

public static class IssueOutboxEventCreator
{
    public static IssueOutboxEvent Create(Issue issue, IssueOutboxEventType eventType)
    {
        if (string.IsNullOrWhiteSpace(issue.JiraId))
        {
            var (title, jiraId) = TemporaryJiraIdRemover.SeparateTitle(issue.Title);
            issue.Title = title;
            issue.JiraId = jiraId;
        }
        
        return new IssueOutboxEvent
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            Type = eventType,
            IssueId = issue.Id,
            Title = issue.Title,
            JiraId = issue.JiraId,
            Description = issue.Description,
            IssueStatus = issue.Status.ToString(),
            AssignedToAccountId = issue.AssignedToAccountId
        };
    }
}