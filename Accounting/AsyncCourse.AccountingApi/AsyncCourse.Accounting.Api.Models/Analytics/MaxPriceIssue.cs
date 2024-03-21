namespace AsyncCourse.Accounting.Api.Models.Analytics;

public class MaxPriceIssue
{
    public Guid Id { get; set; }
    
    // если в будущем захочется узнавать в рамках какой задачи/транзакции
    public Guid TransactionId { get; set; }
    public Guid IssueId { get; set; }
    
    // данные для показа
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}