using AsyncCourse.Issues.Api.Db;
using AsyncCourse.Issues.Api.Db.Dbos;
using AsyncCourse.Issues.Api.Models.Accounts;
using Microsoft.EntityFrameworkCore;

namespace AsyncCourse.Issues.Api.Domain.Repositories.Accounts;

public class IssueAccountRepository : IIssueAccountRepository
{
    private readonly IssuesApiDbContext issuesApiDbContext;

    public IssueAccountRepository(Core.Db.DbContextSupport.IDbContextFactory<IssuesApiDbContext> contextFactory)
    {
        issuesApiDbContext = contextFactory.CreateDbContext();
    }
    
    public async Task CreateAsync(IssueAccount issueAccount)
    {
        await issuesApiDbContext.Accounts.AddAsync(DomainToDbo(issueAccount));

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
        
        // todo могло измениться еще что-то
        
        issuesApiDbContext.Accounts.Update(existingAccount);

        await issuesApiDbContext.SaveChangesAsync();
    }

    public async Task<List<IssueAccount>> GetAccountsAsync(IEnumerable<Guid> accountIds = null)
    {
        var result = await issuesApiDbContext.Accounts.ToListAsync();
        if (accountIds != null)
        {
            result = result.Where(x => accountIds.Contains(x.AccountId)).ToList();
        }

        var mappedResult = result.Select(DboToDomain).ToList();

        return mappedResult;
    }

    #region Mapping

    private static IssueAccountDbo DomainToDbo(IssueAccount issueAccount)
    {
        return new IssueAccountDbo
        {
            AccountId = issueAccount.AccountId,
            Email = issueAccount.Email,
            Name = issueAccount.Name,
            Surname = issueAccount.Surname,
            Role = issueAccount.Role
        };
    }

    private static IssueAccount DboToDomain(IssueAccountDbo dbo)
    {
        return new IssueAccount
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