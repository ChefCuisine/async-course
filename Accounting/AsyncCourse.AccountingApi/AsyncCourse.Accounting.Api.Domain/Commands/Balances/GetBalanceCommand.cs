using AsyncCourse.Accounting.Api.Domain.Repositories.Accounts;
using AsyncCourse.Accounting.Api.Domain.Repositories.Balances;
using AsyncCourse.Accounting.Api.Domain.Repositories.Issues;
using AsyncCourse.Accounting.Api.Domain.Repositories.Transactions;
using AsyncCourse.Accounting.Api.Models.Accounts;
using AsyncCourse.Accounting.Api.Models.Balances;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Balances;

public interface IGetBalanceCommand
{
    Task<AccountBalanceInfo> GetBalanceAsync(Guid accountId, DateTime? dateTime = null);
}

public class GetBalanceCommand : IGetBalanceCommand
{
    private readonly IAccountBalanceRepository accountBalanceRepository;
    private readonly IAccountRepository accountRepository;
    private readonly ITransactionRepository transactionRepository;
    private readonly IIssueRepository issueRepository;

    public GetBalanceCommand(
        IAccountBalanceRepository accountBalanceRepository,
        IAccountRepository accountRepository,
        ITransactionRepository transactionRepository,
        IIssueRepository issueRepository)
    {
        this.accountBalanceRepository = accountBalanceRepository;
        this.accountRepository = accountRepository;
        this.transactionRepository = transactionRepository;
        this.issueRepository = issueRepository;
    }

    public async Task<AccountBalanceInfo> GetBalanceAsync(Guid accountId, DateTime? dateTime = null)
    {
        var account = await accountRepository.GetAsync(accountId);
        if (account == null)
        {
            return null;
        }

        // todo потом сделаем красиво через Authorize какой-нибудь
        if (account.Role == AccountingAccountRole.Unknown)
        {
            return null;
        }
        
        if (!dateTime.HasValue)
        {
            dateTime = DateTime.Now;
        }

        var existingBalance = await accountBalanceRepository.GetAsync(accountId, dateTime.Value);
        if (existingBalance == null)
        {
            return null;
        }

        var response = new AccountBalanceInfo
        {
            AccountId = existingBalance.AccountId,
            Surname = account.Surname,
            Name = account.Name,
            Date = existingBalance.Date,
            Total = existingBalance.Total
        };

        var transactions = await transactionRepository.GetForBalanceAsync(accountId, dateTime.Value);
        if (!transactions.Any())
        {
            return response;
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

        response.Transactions = transactions
            .Select(transaction => new TransactionBalanceInfo
            {
                CreatedAt = transaction.CreatedAt,
                Type = transaction.Type,
                Amount = transaction.Amount,
                IssueInfo = issuesInfo[transaction.IssueInfo.IssueId],
            })
            .ToList();

        return response;
    }
}