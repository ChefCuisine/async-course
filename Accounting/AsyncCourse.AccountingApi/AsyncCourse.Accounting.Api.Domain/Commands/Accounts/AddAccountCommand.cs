using AsyncCourse.Accounting.Api.Domain.Repositories.Accounts;
using AsyncCourse.Accounting.Api.Domain.Repositories.Balances;
using AsyncCourse.Accounting.Api.Models.Accounts;
using AsyncCourse.Accounting.Api.Models.Balances;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Accounts;

public interface IAddAccountCommand
{
    Task AddAsync(AccountingAccount account);
}

public class AddAccountCommand : IAddAccountCommand
{
    private readonly IAccountRepository accountRepository;
    private readonly IAccountBalanceRepository accountBalanceRepository;

    public AddAccountCommand(
        IAccountRepository accountRepository,
        IAccountBalanceRepository accountBalanceRepository)
    {
        this.accountRepository = accountRepository;
        this.accountBalanceRepository = accountBalanceRepository;
    }
    
    public async Task AddAsync(AccountingAccount account)
    {
        await accountRepository.CreateAsync(account);
        await accountBalanceRepository.CreateAsync(new AccountBalance
        {
            Id = Guid.NewGuid(),
            AccountId = account.AccountId,
            Total = 0
        });
    }
}