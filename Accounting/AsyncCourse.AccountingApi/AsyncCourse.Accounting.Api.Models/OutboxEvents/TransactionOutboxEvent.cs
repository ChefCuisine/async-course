namespace AsyncCourse.Accounting.Api.Models.OutboxEvents;

public class TransactionOutboxEvent
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public TransactionOutboxEventType Type { get; set; }
    
    public Guid TransactionId { get; set; }
    
    public decimal Amount { get; set; }
}