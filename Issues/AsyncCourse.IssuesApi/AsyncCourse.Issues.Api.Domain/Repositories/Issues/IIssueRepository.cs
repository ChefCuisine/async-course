using AsyncCourse.Issues.Api.Models.Issues;

namespace AsyncCourse.Issues.Api.Domain.Repositories.Issues;

public interface IIssueRepository
{
    Task<List<Issue>> GetListAsync();
    
    Task<Issue> GetAsync(Guid id);
    
    Task AddAsync(Issue issue);
    
    Task AddBatchAsync(IEnumerable<Issue> issues);

    Task<Issue> UpdateAsync(Issue issue);

    Task UpdateBatchAsync(IEnumerable<Issue> issues);

    Task DeleteAsync(Guid id);
}