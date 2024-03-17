namespace AsyncCourse.Template.Kafka.MessageBus.Models.AccountBalances;

public class MessageBusAccountBalance
{
    public Guid AccountId { get; set; }
    public decimal Total { get; set; }
    public DateTime Date { get; set; }
}