namespace AsyncCourse.Template.Kafka.MessageBus.Models.Issues;

public class MessageBusIssue
{
    public Guid IssueId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public Guid? AssignedToAccountId { get; set; }
}