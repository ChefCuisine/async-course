using AsyncCourse.Accounting.Api.Models.Issues;
using AsyncCourse.Accounting.Api.Models.Transactions;

namespace AsyncCourse.Accounting.Api.Models.Balances;

public class AccountBalanceInfo
{
    public Guid AccountId { get; set; }
    public string Surname { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public decimal? Total { get; set; }
    public List<TransactionBalanceInfo> Transactions { get; set; } = new();
}

public class TransactionBalanceInfo
{
    public DateTime CreatedAt { get; set; }
    public TransactionType Type { get; set; }
    public decimal? Amount { get; set; }
    public IssueBalanceInfo IssueInfo { get; set; }
}

public class IssueBalanceInfo
{
    public string JiraId { get; set; }
    public string Title { get; set; }
    public AccountingIssueStatus Status { get; set; }
}