namespace AsyncCourse.Accounting.Api.Models.Transactions;

public class IssueTransactionInfo
{
    public Guid IssueId { get; set; }
    public Guid AssignToAccountId { get; set; }
}