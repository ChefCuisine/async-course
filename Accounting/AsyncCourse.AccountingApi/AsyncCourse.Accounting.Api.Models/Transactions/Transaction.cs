namespace AsyncCourse.Accounting.Api.Models.Transactions;

public class Transaction
{
    public Guid Id { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public TransactionType Type { get; set; }

    public IssueTransactionInfo IssueInfo { get; set; }
    
    public ClosedDayInfo ClosedDayInfo { get; set; }
    
    public decimal? Amount { get; set; }
}