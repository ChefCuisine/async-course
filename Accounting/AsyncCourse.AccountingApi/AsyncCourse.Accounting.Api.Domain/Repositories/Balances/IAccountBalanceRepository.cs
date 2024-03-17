using AsyncCourse.Accounting.Api.Models.Balances;

namespace AsyncCourse.Accounting.Api.Domain.Repositories.Balances;

public interface IAccountBalanceRepository
{
    Task<AccountBalance> GetAsync(Guid accountId, DateTime dateTime);
    
    Task<List<AccountBalance>> GetAllAsync(DateTime dateTime);

    Task<List<AccountBalance>> GetForPeriodAsync(Guid accountId, DateTime from, DateTime to);

    Task CreateAsync(AccountBalance accountBalance);

    Task UpdateAsync(AccountBalance accountBalance);

    Task UpdateAsync(Guid accountId, DateTime dateTime, decimal? amountToAdd = null);
}