using System.ComponentModel.DataAnnotations.Schema;

namespace AsyncCourse.Accounting.Api.Db.Dbos;

[Table("max-price-issues")]
public class MaxPriceIssueDbo
{
    [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    
    [Column("transaction_id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid TransactionId { get; set; }
    
    [Column("issue_id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid IssueId { get; set; }
    
    [Column("amount")]
    public decimal Amount { get; set; }
    
    [Column("date", TypeName = "date")]
    public DateTime Date { get; set; }
}