using AsyncCourse.Accounting.Api.Models.Accounts;

namespace AsyncCourse.Accounting.Api.Domain.Repositories.Accounts;

public interface IAccountRepository
{
    Task<AccountingAccount> GetAsync(Guid id);

    Task CreateAsync(AccountingAccount account);

    Task UpdateAsync(AccountingAccount account);

    Task<List<AccountingAccount>> GetAccountsAsync();
}