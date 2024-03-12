namespace AsyncCourse.Template.Kafka.MessageBus.Models.Events;

public class MessageBusEventMetaInfo
{
    public string EventId { get; set; }
    public string EventType { get; set; }
    public int? EventVersion { get; set; }
    public string EventDateTime { get; set; }
    public string ProducerName { get; set; }
}