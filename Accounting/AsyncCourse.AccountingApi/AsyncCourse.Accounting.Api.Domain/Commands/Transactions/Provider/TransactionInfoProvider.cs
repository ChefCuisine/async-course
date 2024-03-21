using AsyncCourse.Accounting.Api.Domain.Repositories.Issues;
using AsyncCourse.Accounting.Api.Domain.Repositories.Transactions;
using AsyncCourse.Accounting.Api.Models.Balances;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Transactions.Provider;

public interface ITransactionInfoProvider
{
    Task<List<TransactionBalanceInfo>> GetTransactionBalanceInfosAsync(Guid accountId, DateTime dateTime);
}

public class TransactionInfoProvider : ITransactionInfoProvider
{
    private readonly ITransactionRepository transactionRepository;
    private readonly IIssueRepository issueRepository;

    public TransactionInfoProvider(ITransactionRepository transactionRepository, IIssueRepository issueRepository)
    {
        this.transactionRepository = transactionRepository;
        this.issueRepository = issueRepository;
    }

    public async Task<List<TransactionBalanceInfo>> GetTransactionBalanceInfosAsync(Guid accountId, DateTime dateTime)
    {
        var transactions = await transactionRepository.GetForBalanceAsync(accountId, dateTime);
        if (!transactions.Any())
        {
            return new List<TransactionBalanceInfo>();
        }

        var issueIds = transactions.Select(x => x.IssueInfo.IssueId).Distinct().ToList();
        var issues = await issueRepository.GetListAsync(issueIds);

        var issuesInfo = issues.ToDictionary(
            k => k.IssueId,
            issue => new IssueBalanceInfo
            {
                JiraId = issue.JiraId,
                Title = issue.Title,
                Status = issue.Status
            });
        
        return transactions
            .Select(transaction => new TransactionBalanceInfo
            {
                CreatedAt = transaction.CreatedAt,
                Type = transaction.Type,
                Amount = transaction.Amount,
                IssueInfo = issuesInfo[transaction.IssueInfo.IssueId],
            })
            .ToList();
    }
}