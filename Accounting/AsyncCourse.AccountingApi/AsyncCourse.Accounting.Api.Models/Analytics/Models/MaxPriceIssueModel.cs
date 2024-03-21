namespace AsyncCourse.Accounting.Api.Models.Analytics.Models;

public class MaxPriceIssueModel
{
    public Guid Id { get; set; }
    public Guid TransactionId { get; set; }
    public Guid IssueId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}