using AsyncCourse.Issues.Api.Models.OutboxEvents;

namespace AsyncCourse.Issues.Api.Domain.Repositories.OutboxEvents;

public interface IIssueOutboxEventRepository
{
    Task AddAsync(IssueOutboxEvent issue);
    Task AddBatchAsync(IEnumerable<IssueOutboxEvent> issues);
    Task<IssueOutboxEvent> GetAsync(Guid id);
    Task<IssueOutboxEvent> GetNextAsync();
    Task DeleteAsync(Guid id);
}