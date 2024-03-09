using AsyncCourse.Template.Kafka.MessageBus.Models.Issues;

namespace AsyncCourse.Template.Kafka.MessageBus.Models.Events.Issues;

public class MessageBusIssuesEvent : MessageBusEvent
{
    public MessageBusBusinessChangedIssue Context { get; set; }
}