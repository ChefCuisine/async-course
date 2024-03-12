using AsyncCourse.Template.Kafka.MessageBus.Models.Issues;

namespace AsyncCourse.Template.Kafka.MessageBus.Models.Events.Issues;

public class MessageBusIssuesStreamEvent : MessageBusEvent
{
    public MessageBusIssue Context { get; set; }
}