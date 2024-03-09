using AsyncCourse.Accounting.Api.Domain.Repositories;
using AsyncCourse.Accounting.Api.Domain.Repositories.Accounts;
using AsyncCourse.Accounting.Api.Models.Accounts;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Accounts;

public interface IUpdateAccountCommand
{
    Task UpdateAsync(AccountingAccount account);
}

public class UpdateAccountCommand : IUpdateAccountCommand
{
    private readonly IAccountRepository accountRepository;

    public UpdateAccountCommand(IAccountRepository accountRepository)
    {
        this.accountRepository = accountRepository;
    }
    
    public async Task UpdateAsync(AccountingAccount account)
    {
        await accountRepository.UpdateAsync(account);
    }
}