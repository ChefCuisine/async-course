namespace AsyncCourse.Template.Kafka.MessageBus.Models.Accounts;

public enum MessageBusAccountStreamEventType
{
    Unknown = 0,
    Created = 1,
    Updated = 2,
    Deleted = 3,
}