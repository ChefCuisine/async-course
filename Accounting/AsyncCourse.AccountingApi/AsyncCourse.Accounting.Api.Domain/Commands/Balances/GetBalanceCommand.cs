using AsyncCourse.Accounting.Api.Domain.Commands.Transactions.Provider;
using AsyncCourse.Accounting.Api.Domain.Repositories.Accounts;
using AsyncCourse.Accounting.Api.Domain.Repositories.Balances;
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
    private readonly ITransactionInfoProvider transactionInfoProvider;

    public GetBalanceCommand(
        IAccountBalanceRepository accountBalanceRepository,
        IAccountRepository accountRepository,
        ITransactionInfoProvider transactionInfoProvider)
    {
        this.accountBalanceRepository = accountBalanceRepository;
        this.accountRepository = accountRepository;
        this.transactionInfoProvider = transactionInfoProvider;
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

        response.Transactions = await transactionInfoProvider.GetTransactionBalanceInfosAsync(accountId, dateTime.Value);

        return response;
    }
}