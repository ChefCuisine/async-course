namespace AsyncCourse.Template.Kafka.MessageBus.Models.Accounts;

public class MessageBusIssue
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public Guid? AccountId { get; set; }
}