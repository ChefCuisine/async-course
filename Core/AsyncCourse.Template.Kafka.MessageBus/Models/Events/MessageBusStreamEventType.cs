namespace AsyncCourse.Template.Kafka.MessageBus.Models.Events;

public enum MessageBusStreamEventType
{
    Unknown = 0,
    Created = 1,
    Updated = 2,
    Deleted = 3,
}