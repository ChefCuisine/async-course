namespace AsyncCourse.Issues.Api.Models.OutboxEvents;

public enum IssueOutboxEventType
{
    Unknown = 0,
    Created = 1,
    Reassigned = 2,
    Done = 3,
}