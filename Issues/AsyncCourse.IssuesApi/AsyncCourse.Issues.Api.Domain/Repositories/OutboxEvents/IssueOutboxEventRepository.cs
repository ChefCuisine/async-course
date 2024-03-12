using AsyncCourse.Issues.Api.Db;
using AsyncCourse.Issues.Api.Db.Dbos;
using AsyncCourse.Issues.Api.Models.OutboxEvents;

namespace AsyncCourse.Issues.Api.Domain.Repositories.OutboxEvents;

public class IssueOutboxEventRepository : IIssueOutboxEventRepository
{
    private readonly IssuesApiDbContext issuesApiDbContext;

    public IssueOutboxEventRepository(Core.Db.DbContextSupport.IDbContextFactory<IssuesApiDbContext> contextFactory)
    {
        issuesApiDbContext = contextFactory.CreateDbContext();
    }

    public async Task AddAsync(IssueOutboxEvent issue)
    {
        await issuesApiDbContext.IssueEvents.AddAsync(DomainToDbo(issue));

        await issuesApiDbContext.SaveChangesAsync();
    }
    
    public async Task AddBatchAsync(IEnumerable<IssueOutboxEvent> issues)
    {
        var mappedDbos = issues.Select(DomainToDbo);
        await issuesApiDbContext.IssueEvents.AddRangeAsync(mappedDbos);

        await issuesApiDbContext.SaveChangesAsync();
    }

    public async Task<IssueOutboxEvent> GetAsync(Guid id)
    {
        var dbo = await issuesApiDbContext.IssueEvents.FindAsync(id);

        if (dbo != null)
        {
            return DboToDomain(dbo);
        }

        return null;
    }

    public async Task<IssueOutboxEvent> GetNextAsync()
    {
        var dbo = issuesApiDbContext.IssueEvents
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefault();
        
        if (dbo != null)
        {
            return DboToDomain(dbo);
        }

        return null;
    }

    public async Task DeleteAsync(Guid id)
    {
        var dbo = await issuesApiDbContext.IssueEvents.FindAsync(id);

        if (dbo != null)
        {
            return;
        }

        issuesApiDbContext.IssueEvents.Remove(dbo);
        await issuesApiDbContext.SaveChangesAsync();
    }
    
    #region Mapping

    private static IssueOutboxEventDbo DomainToDbo(IssueOutboxEvent issueEvent)
    {
        return new IssueOutboxEventDbo
        {
            Id = issueEvent.Id == Guid.Empty ? Guid.NewGuid() : issueEvent.Id,
            CreatedAt = issueEvent.CreatedAt,
            Type = issueEvent.Type,
            IssueId = issueEvent.IssueId,
            Title = issueEvent.Title,
            Description = issueEvent.Description,
            IssueStatus = issueEvent.IssueStatus,
            AssignedToAccountId = issueEvent.AssignedToAccountId,
        };
    }

    private static IssueOutboxEvent DboToDomain(IssueOutboxEventDbo dbo)
    {
        return new IssueOutboxEvent
        {
            Id = dbo.Id,
            CreatedAt = dbo.CreatedAt,
            Type = dbo.Type,
            IssueId = dbo.Id,
            Title = dbo.Title,
            Description = dbo.Description,
            IssueStatus = dbo.IssueStatus,
            AssignedToAccountId = dbo.AssignedToAccountId,
        };
    }

    #endregion
}