using AsyncCourse.Auth.Api.Db;
using AsyncCourse.Auth.Api.Db.Dbos;
using AsyncCourse.Auth.Api.Models.Accounts;
using Microsoft.EntityFrameworkCore;

namespace AsyncCourse.Auth.Api.Domain.Repositories;

public interface IAccountRepository
{
    Task<List<AuthAccount>> GetListAsync();
    
    Task<AuthAccount> GetByLoginAndPasswordAsync(string email, string password);
    Task<AuthAccount> GetByIdAsync(Guid id);

    Task AddAsync(AuthAccount account);
    Task<AuthAccount> EditAsync(EditAuthAccount account);
}

public class AccountRepository : IAccountRepository
{
    private readonly AuthApiDbContext authApiDbContext;
    
    public AccountRepository(Core.Db.DbContextSupport.IDbContextFactory<AuthApiDbContext> contextFactory)
    {
        authApiDbContext = contextFactory.CreateDbContext();
    }
    
    public async Task<List<AuthAccount>> GetListAsync()
    {
        var result = await authApiDbContext.Accounts.ToListAsync();
        var mappedResult = result.Select(DboToDomain).ToList();

        return mappedResult;
    }
    
    public async Task<AuthAccount> GetByLoginAndPasswordAsync(string email, string password)
    {
        var dbo = authApiDbContext.Accounts
            .FirstOrDefault(account => account.Email == email && account.Password == password);

        if (dbo != null)
        {
            return DboToDomain(dbo);
        }

        return null;
    }

    public async Task<AuthAccount> GetByIdAsync(Guid id)
    {
        var dbo = await authApiDbContext.Accounts.FindAsync(id);

        if (dbo != null)
        {
            return DboToDomain(dbo);
        }

        return null;
    }
    
    public async Task AddAsync(AuthAccount account)
    {
        await authApiDbContext.Accounts.AddAsync(DomainToDbo(account));

        await authApiDbContext.SaveChangesAsync();
    }

    public async Task<AuthAccount> EditAsync(EditAuthAccount account)
    {
        var existingAccount = await authApiDbContext.Accounts.FindAsync(account.Id);
        if (existingAccount == null)
        {
            return null;
        }

        existingAccount.Role = account.Role == AuthAccountRole.Unknown ? AuthAccountRole.Employee : account.Role;

        authApiDbContext.Accounts.Update(existingAccount);

        await authApiDbContext.SaveChangesAsync();
        
        return DboToDomain(existingAccount);
    }

    #region Mapping

    private static AuthAccountDbo DomainToDbo(AuthAccount account)
    {
        return new AuthAccountDbo
        {
            Id = account.Id == Guid.Empty ? Guid.NewGuid() : account.Id,
            Email = account.Email,
            Password = account.Password,
            Name = account.Name,
            Surname = account.Surname,
            Role = account.Role == AuthAccountRole.Unknown ? AuthAccountRole.Employee : account.Role
        };
    }

    private static AuthAccount DboToDomain(AuthAccountDbo dbo)
    {
        return new AuthAccount
        {
            Id = dbo.Id,
            Email = dbo.Email,
            Name = dbo.Name,
            Surname = dbo.Surname,
            Role = dbo.Role
        };
    }

    #endregion
}