namespace AsyncCourse.Issues.Api.Models.OutboxEvents;

public class IssueOutboxEvent
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public IssueOutboxEventType Type { get; set; }
    
    public Guid IssueId { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }

    public string IssueStatus { get; set; }
    
    public Guid? AssignedToAccountId { get; set; }
}