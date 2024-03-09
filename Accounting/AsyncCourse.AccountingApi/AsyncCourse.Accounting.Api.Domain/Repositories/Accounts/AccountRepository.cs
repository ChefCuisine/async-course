using AsyncCourse.Accounting.Api.Db;
using AsyncCourse.Accounting.Api.Db.Dbos;
using AsyncCourse.Accounting.Api.Models.Accounts;
using Microsoft.EntityFrameworkCore;

namespace AsyncCourse.Accounting.Api.Domain.Repositories.Accounts;

public class AccountRepository : IAccountRepository
{
    private readonly AccountingApiDbContext accountingApiDbContext;

    public AccountRepository(Core.Db.DbContextSupport.IDbContextFactory<AccountingApiDbContext> contextFactory)
    {
        accountingApiDbContext = contextFactory.CreateDbContext();
    }

    public async Task CreateAsync(AccountingAccount account)
    {
        await accountingApiDbContext.Accounts.AddAsync(DomainToDbo(account));

        await accountingApiDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(AccountingAccount account)
    {
        var existingAccount = await accountingApiDbContext.Accounts.FindAsync(account.AccountId);
        if (existingAccount == null)
        {
            return;
        }

        existingAccount.Role = account.Role;
        
        // todo могло измениться еще что-то
        
        accountingApiDbContext.Accounts.Update(existingAccount);

        await accountingApiDbContext.SaveChangesAsync();
    }

    public async Task<List<AccountingAccount>> GetAccountsAsync()
    {
        var result = await accountingApiDbContext.Accounts.ToListAsync();
        var mappedResult = result.Select(DboToDomain).ToList();

        return mappedResult;
    }
    
    #region Mapping

    private static AccountingAccountDbo DomainToDbo(AccountingAccount account)
    {
        return new AccountingAccountDbo
        {
            AccountId = account.AccountId,
            Email = account.Email,
            Name = account.Name,
            Surname = account.Surname,
            Role = account.Role
        };
    }

    private static AccountingAccount DboToDomain(AccountingAccountDbo dbo)
    {
        return new AccountingAccount
        {
            AccountId = dbo.AccountId,
            Email = dbo.Email,
            Name = dbo.Name,
            Surname = dbo.Surname,
            Role = dbo.Role
        };
    }

    #endregion
}