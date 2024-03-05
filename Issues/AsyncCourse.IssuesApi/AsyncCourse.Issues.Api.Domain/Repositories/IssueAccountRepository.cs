using AsyncCourse.Issues.Api.Db;
using AsyncCourse.Issues.Api.Db.Dbos;
using AsyncCourse.Issues.Api.Models.Accounts;
using Microsoft.EntityFrameworkCore;

namespace AsyncCourse.Issues.Api.Domain.Repositories;

public interface IIssueAccountRepository
{
    Task CreateAsync(IssueAccount issueAccount);
    Task UpdateAsync(IssueAccount issueAccount);

    Task<List<IssueAccount>> GetAccountsAsync();
}

public class IssueAccountRepository : IIssueAccountRepository
{
    private readonly IssuesApiDbContext issuesApiDbContext;

    public IssueAccountRepository(Core.Db.DbContextSupport.IDbContextFactory<IssuesApiDbContext> contextFactory)
    {
        issuesApiDbContext = contextFactory.CreateDbContext();
    }
    
    public async Task CreateAsync(IssueAccount issueAccount)
    {
        await issuesApiDbContext.Accounts.AddAsync(new IssueAccountDbo
        {
            AccountId = issueAccount.AccountId,
            Email = issueAccount.Email,
            Name = issueAccount.Name,
            Surname = issueAccount.Surname,
            Role = issueAccount.Role
        });

        await issuesApiDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(IssueAccount issueAccount)
    {
        var existingAccount = await issuesApiDbContext.Accounts.FindAsync(issueAccount.AccountId);
        if (existingAccount == null)
        {
            return;
        }

        existingAccount.Role = issueAccount.Role;
        
        issuesApiDbContext.Accounts.Update(existingAccount);

        await issuesApiDbContext.SaveChangesAsync();
    }

    public async Task<List<IssueAccount>> GetAccountsAsync()
    {
        var result = await issuesApiDbContext.Accounts.ToListAsync();
        var mappedResult = result.Select(dbo => new IssueAccount
        {
            AccountId = dbo.AccountId,
            Email = dbo.Email,
            Name = dbo.Name,
            Surname = dbo.Surname,
            Role = dbo.Role
        }).ToList();

        return mappedResult;
    }
}