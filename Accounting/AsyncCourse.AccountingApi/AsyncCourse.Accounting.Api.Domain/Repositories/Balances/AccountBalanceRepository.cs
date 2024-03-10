using AsyncCourse.Accounting.Api.Db;
using AsyncCourse.Accounting.Api.Db.Dbos;
using AsyncCourse.Accounting.Api.Models.Balances;

namespace AsyncCourse.Accounting.Api.Domain.Repositories.Balances;

public class AccountBalanceRepository : IAccountBalanceRepository
{
    private readonly AccountingApiDbContext accountingApiDbContext;

    public AccountBalanceRepository(Core.Db.DbContextSupport.IDbContextFactory<AccountingApiDbContext> contextFactory)
    {
        accountingApiDbContext = contextFactory.CreateDbContext();
    }

    public async Task<AccountBalance> GetAsync(Guid id)
    {
        var dbo = await accountingApiDbContext.AccountBalances.FindAsync(id);

        if (dbo != null)
        {
            return DboToDomain(dbo);
        }

        return null;
    }

    public async Task CreateAsync(AccountBalance accountBalance)
    {
        await accountingApiDbContext.AccountBalances.AddAsync(DomainToDbo(accountBalance));
        
        await accountingApiDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(AccountBalance accountBalance)
    {
        var existingBalance = await accountingApiDbContext.AccountBalances.FindAsync(accountBalance.AccountId);
        if (existingBalance == null)
        {
            return;
        }

        existingBalance.Total = accountBalance.Total;

        accountingApiDbContext.AccountBalances.Update(existingBalance);

        await accountingApiDbContext.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Guid accountId, decimal? amountToAdd = null)
    {
        var existingBalance = await accountingApiDbContext.AccountBalances.FindAsync(accountId);
        if (existingBalance == null)
        {
            return;
        }

        existingBalance.Total += amountToAdd;

        accountingApiDbContext.AccountBalances.Update(existingBalance);

        await accountingApiDbContext.SaveChangesAsync();
    }
    
    #region Mapping

    private static AccountBalanceDbo DomainToDbo(AccountBalance accountBalance)
    {
        return new AccountBalanceDbo
        {
            Id = accountBalance.Id == Guid.Empty ? Guid.NewGuid() : accountBalance.Id,
            AccountId = accountBalance.AccountId,
            Total = accountBalance.Total,
        };
    }

    private static AccountBalance DboToDomain(AccountBalanceDbo dbo)
    {
        return new AccountBalance
        {
            Id = dbo.Id,
            AccountId = dbo.AccountId,
            Total = dbo.Total,
        };
    }

    #endregion
}