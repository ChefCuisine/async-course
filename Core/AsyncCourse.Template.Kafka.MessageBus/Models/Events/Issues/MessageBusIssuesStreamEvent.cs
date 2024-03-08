using AsyncCourse.Template.Kafka.MessageBus.Models.Issues;

namespace AsyncCourse.Template.Kafka.MessageBus.Models.Events.Issues;

public class MessageBusIssuesStreamEvent
{
    public MessageBusIssueStreamEventType Type { get; set; }
    public MessageBusIssue Context { get; set; }
}