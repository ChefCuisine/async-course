using System.ComponentModel.DataAnnotations.Schema;
using AsyncCourse.Accounting.Api.Models.OutboxEvents;

namespace AsyncCourse.Accounting.Api.Db.Dbos;

[Table("transaction-events")]
public class TransactionOutboxEventDbo
{
    [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    
    [Column("created_at", TypeName = "date")]
    public DateTime CreatedAt { get; set; }
    
    [Column("type")]
    public TransactionOutboxEventType Type { get; set; }
    
    [Column("transaction_id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid TransactionId { get; set; }
}