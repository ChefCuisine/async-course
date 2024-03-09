using AsyncCourse.Accounting.Api.Domain.Repositories;
using AsyncCourse.Accounting.Api.Domain.Repositories.Accounts;
using AsyncCourse.Accounting.Api.Models.Accounts;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Accounts;

public interface IAddAccountCommand
{
    Task AddAsync(AccountingAccount account);
}

public class AddAccountCommand : IAddAccountCommand
{
    private readonly IAccountRepository accountRepository;

    public AddAccountCommand(IAccountRepository accountRepository)
    {
        this.accountRepository = accountRepository;
    }
    
    public async Task AddAsync(AccountingAccount account)
    {
        await accountRepository.CreateAsync(account);
    }
}