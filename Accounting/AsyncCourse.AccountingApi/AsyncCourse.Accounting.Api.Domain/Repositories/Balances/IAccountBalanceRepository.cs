using AsyncCourse.Accounting.Api.Models.Balances;

namespace AsyncCourse.Accounting.Api.Domain.Repositories.Balances;

public interface IAccountBalanceRepository
{
    Task<AccountBalance> GetAsync(Guid id);

    Task CreateAsync(AccountBalance accountBalance);

    Task UpdateAsync(AccountBalance accountBalance);

    Task UpdateAsync(Guid accountId, decimal? amountToAdd = null);
}