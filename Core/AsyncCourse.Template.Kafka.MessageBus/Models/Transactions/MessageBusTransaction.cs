namespace AsyncCourse.Template.Kafka.MessageBus.Models.Transactions;

public class MessageBusTransaction
{
    public Guid TransactionId { get; set; }
    public MessageBusTransactionAnalyticsInfo AnalyticsInfo { get; set; }
}

public class MessageBusTransactionAnalyticsInfo
{
    public DateTime CreatedAt { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; }
}