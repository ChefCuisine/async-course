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
        var mappedResult = result.Select(dbo => new Issue
        {
            Id = dbo.Id,
            Title = dbo.Title,
            Description = dbo.Description,
            Status = dbo.Status,
        }).ToList();

        return mappedResult;
    }
    
    public async Task<Issue> GetAsync(Guid id)
    {
        var dbo = await issuesApiDbContext.Issues.FindAsync(id);

        if (dbo != null)
        {
            return new Issue
            {
                Id = dbo.Id,
                Title = dbo.Title,
                Description = dbo.Description,
                Status = dbo.Status,
                AccountId = dbo.AccountId
            };
        }

        return null;
    }
    
    public async Task AddAsync(Issue issue)
    {
        await issuesApiDbContext.Issues.AddAsync(new IssueDbo
        {
            Id = issue.Id == Guid.Empty ? Guid.NewGuid() : issue.Id,
            Title = issue.Title,
            Description = issue.Description,
            Status = issue.Status == IssueStatus.Unknown ? IssueStatus.Created : issue.Status,
            AccountId = issue.AccountId,
        });

        await issuesApiDbContext.SaveChangesAsync();
    }
}