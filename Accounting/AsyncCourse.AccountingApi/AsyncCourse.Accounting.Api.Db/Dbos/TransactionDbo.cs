using System.ComponentModel.DataAnnotations.Schema;
using AsyncCourse.Accounting.Api.Models.Transactions;

namespace AsyncCourse.Accounting.Api.Db.Dbos;

[Table("transactions")]
public class TransactionDbo
{
    [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    
    [Column("created_at", TypeName = "date")]
    public DateTime CreatedAt { get; set; }

    [Column("type")]
    public TransactionType Type { get; set; }

    [Column("issue_info", TypeName = "jsonb")]
    public IssueTransactionInfo IssueInfo { get; set; }
    
    [Column("amount")]
    public decimal? Amount { get; set; }
}