namespace AsyncCourse.Accounting.Api.Models.Balances.Models;

public class AccountBalanceInfoModel
{
    public Guid AccountId { get; set; }
    public string Surname { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public decimal? Total { get; set; }
    public List<TransactionBalanceInfoModel> Transactions { get; set; } = new();
}

public class TransactionBalanceInfoModel
{
    public DateTime CreatedAt { get; set; }
    public string Type { get; set; }
    public decimal? Amount { get; set; }
    public IssueBalanceInfoModel IssueInfo { get; set; }
}

public class IssueBalanceInfoModel
{
    public string JiraId { get; set; }
    public string Title { get; set; }
    public string Status { get; set; }
}