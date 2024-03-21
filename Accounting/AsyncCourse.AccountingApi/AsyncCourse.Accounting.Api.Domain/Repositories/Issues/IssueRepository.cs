using AsyncCourse.Accounting.Api.Db;
using AsyncCourse.Accounting.Api.Db.Dbos;
using AsyncCourse.Accounting.Api.Models.Issues;

namespace AsyncCourse.Accounting.Api.Domain.Repositories.Issues;

public class IssueRepository : IIssueRepository
{
    private readonly AccountingApiDbContext accountingApiDbContext;

    public IssueRepository(Core.Db.DbContextSupport.IDbContextFactory<AccountingApiDbContext> contextFactory)
    {
        accountingApiDbContext = contextFactory.CreateDbContext();
    }
    
    public async Task<List<AccountingIssue>> GetListAsync(List<Guid> ids = null)
    {
        IQueryable<AccountingIssueDbo> result;
        if (ids != null)
        {
            result = accountingApiDbContext.Issues.Where(issue => ids.Contains(issue.IssueId));
        }
        else
        {
            result = accountingApiDbContext.Set<AccountingIssueDbo>();
        }

        var mappedResult = result.AsEnumerable().Select(DboToDomain).ToList();

        return mappedResult;
    }

    public async Task<AccountingIssue> GetAsync(Guid id)
    {
        var dbo = await accountingApiDbContext.Issues.FindAsync(id);

        if (dbo != null)
        {
            return DboToDomain(dbo);
        }

        return null;
    }

    public async Task AddAsync(AccountingIssue issue)
    {
        await accountingApiDbContext.Issues.AddAsync(DomainToDbo(issue));

        await accountingApiDbContext.SaveChangesAsync();
    }

    public async Task AddBatchAsync(IEnumerable<AccountingIssue> issues)
    {
        var mappedDbos = issues.Select(DomainToDbo);
        await accountingApiDbContext.Issues.AddRangeAsync(mappedDbos);

        await accountingApiDbContext.SaveChangesAsync();
    }

    public async Task<AccountingIssue> UpdateAsync(AccountingIssue issue)
    {
        var existingIssue = await accountingApiDbContext.Issues.FindAsync(issue.IssueId);
        if (existingIssue == null)
        {
            return null;
        }

        if (existingIssue.Status != issue.Status && issue.Status != AccountingIssueStatus.Unknown)
        {
            existingIssue.Status = issue.Status;
        }

        existingIssue.AssignedToAccountId = issue.AssignedToAccountId;
        existingIssue.Title = issue.Title;
        existingIssue.Description = issue.Description;

        accountingApiDbContext.Issues.Update(existingIssue);

        await accountingApiDbContext.SaveChangesAsync();
        
        return DboToDomain(existingIssue);
    }

    public async Task DeleteAsync(Guid id)
    {
        var dbo = await accountingApiDbContext.Issues.FindAsync(id);

        if (dbo != null)
        {
            return;
        }

        accountingApiDbContext.Issues.Remove(dbo);
        await accountingApiDbContext.SaveChangesAsync();
    }
    
    #region Mapping

    private static AccountingIssueDbo DomainToDbo(AccountingIssue issue)
    {
        return new AccountingIssueDbo
        {
            IssueId = issue.IssueId == Guid.Empty ? Guid.NewGuid() : issue.IssueId,
            Title = issue.Title,
            JiraId = issue.JiraId,
            Description = issue.Description,
            Status = issue.Status == AccountingIssueStatus.Unknown ? AccountingIssueStatus.Created : issue.Status,
            AssignedToAccountId = issue.AssignedToAccountId,
        };
    }

    private static AccountingIssue DboToDomain(AccountingIssueDbo dbo)
    {
        return new AccountingIssue
        {
            IssueId = dbo.IssueId,
            Title = dbo.Title,
            JiraId = dbo.JiraId,
            Description = dbo.Description,
            Status = dbo.Status,
            AssignedToAccountId = dbo.AssignedToAccountId,
        };
    }

    #endregion
}