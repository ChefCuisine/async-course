using AsyncCourse.Issues.Api.Db;
using AsyncCourse.Issues.Api.Db.Dbos;
using AsyncCourse.Issues.Api.Models.Issues;
using Microsoft.EntityFrameworkCore;

namespace AsyncCourse.Issues.Api.Domain.Repositories;

public interface IIssueRepository
{
    Task<List<Issue>> GetListAsync();
    
    Task<Issue> GetAsync(Guid id);
    
    Task AddAsync(Issue issue);
    
    Task AddBatchAsync(IEnumerable<Issue> issues);

    Task<Issue> Update(Issue issue);
}

public class IssueRepository : IIssueRepository
{
    private readonly IssuesApiDbContext issuesApiDbContext;

    public IssueRepository(Core.Db.DbContextSupport.IDbContextFactory<IssuesApiDbContext> contextFactory)
    {
        issuesApiDbContext = contextFactory.CreateDbContext();
    }
    
    public async Task<List<Issue>> GetListAsync()
    {
        var result = await issuesApiDbContext.Issues.ToListAsync();
        var mappedResult = result.Select(DboToDomain).ToList();

        return mappedResult;
    }
    
    public async Task<Issue> GetAsync(Guid id)
    {
        var dbo = await issuesApiDbContext.Issues.FindAsync(id);

        if (dbo != null)
        {
            return DboToDomain(dbo);
        }

        return null;
    }
    
    public async Task AddAsync(Issue issue)
    {
        await issuesApiDbContext.Issues.AddAsync(DomainToDbo(issue));

        await issuesApiDbContext.SaveChangesAsync();
    }

    public async Task AddBatchAsync(IEnumerable<Issue> issues)
    {
        var mappedDbos = issues.Select(DomainToDbo);
        await issuesApiDbContext.Issues.AddRangeAsync(mappedDbos);

        await issuesApiDbContext.SaveChangesAsync();
    }

    public async Task<Issue> Update(Issue issue)
    {
        var existingIssue = await issuesApiDbContext.Issues.FindAsync(issue.Id);
        if (existingIssue == null)
        {
            return null;
        }

        if (existingIssue.Status != issue.Status && issue.Status != IssueStatus.Unknown)
        {
            existingIssue.Status = issue.Status;
        }
        
        // todo поменяли заголовок, описание, ответственного

        issuesApiDbContext.Issues.Update(existingIssue);

        await issuesApiDbContext.SaveChangesAsync();
        
        return DboToDomain(existingIssue);
    }

    #region Mapping

    private static IssueDbo DomainToDbo(Issue issue)
    {
        return new IssueDbo
        {
            Id = issue.Id == Guid.Empty ? Guid.NewGuid() : issue.Id,
            Title = issue.Title,
            Description = issue.Description,
            Status = issue.Status == IssueStatus.Unknown ? IssueStatus.Created : issue.Status,
            AssignedToAccountId = issue.AssignedToAccountId,
        };
    }

    private static Issue DboToDomain(IssueDbo dbo)
    {
        return new Issue
        {
            Id = dbo.Id,
            Title = dbo.Title,
            Description = dbo.Description,
            Status = dbo.Status,
            AssignedToAccountId = dbo.AssignedToAccountId,
        };
    }

    #endregion
}