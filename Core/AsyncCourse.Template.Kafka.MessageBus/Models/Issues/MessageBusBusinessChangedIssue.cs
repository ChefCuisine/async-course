namespace AsyncCourse.Template.Kafka.MessageBus.Models.Issues;

public class MessageBusBusinessChangedIssue
{
    public Guid IssueId { get; set; }
    public Guid? AssignedToAccountId { get; set; }
}