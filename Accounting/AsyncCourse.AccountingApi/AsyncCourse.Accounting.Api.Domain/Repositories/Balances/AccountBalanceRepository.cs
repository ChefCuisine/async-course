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

    public async Task<AccountBalance> GetAsync(Guid accountId, DateTime dateTime)
    {
        var dbo = await accountingApiDbContext.AccountBalances.FindAsync(accountId, dateTime);
        if (dbo != null)
        {
            return DboToDomain(dbo);
        }

        return null;
    }

    public async Task<List<AccountBalance>> GetForPeriodAsync(Guid accountId, DateTime from, DateTime to)
    {
        var result = new List<AccountBalance>();

        while (from < to)
        {
            var dbo = await accountingApiDbContext.AccountBalances.FindAsync(accountId, from);
            if (dbo != null)
            {
                result.Add(DboToDomain(dbo));
                from = from.AddDays(1);
            }
        }

        return result;
    }

    public async Task CreateAsync(AccountBalance accountBalance)
    {
        await accountingApiDbContext.AccountBalances.AddAsync(DomainToDbo(accountBalance));
        
        await accountingApiDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(AccountBalance accountBalance)
    {
        var existingBalance = await accountingApiDbContext.AccountBalances.FindAsync(accountBalance.AccountId, accountBalance.Date);
        if (existingBalance == null)
        {
            existingBalance = DomainToDbo(accountBalance);
        }
        else
        {
            existingBalance.Total = accountBalance.Total;
        }

        accountingApiDbContext.AccountBalances.Update(existingBalance);

        await accountingApiDbContext.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Guid accountId, DateTime dateTime, decimal? amountToAdd = null)
    {
        var existingBalance = await accountingApiDbContext.AccountBalances.FindAsync(accountId, dateTime);
        if (existingBalance == null)
        {
            existingBalance = new AccountBalanceDbo
            {
                Id = Guid.NewGuid(),
                AccountId = accountId,
                Date = dateTime,
                Total = 0,
            };
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
            Date = accountBalance.Date,
            Total = accountBalance.Total,
        };
    }

    private static AccountBalance DboToDomain(AccountBalanceDbo dbo)
    {
        return new AccountBalance
        {
            Id = dbo.Id,
            AccountId = dbo.AccountId,
            Date = dbo.Date,
            Total = dbo.Total,
        };
    }

    #endregion
}