using AsyncCourse.Template.Kafka.MessageBus.Models.Issues;

namespace AsyncCourse.Template.Kafka.MessageBus.Models.Events.Issues;

public class MessageBusIssuesEvent
{
    public MessageBusIssuesEventType Type { get; set; }
    public MessageBusIssue Context { get; set; }
}