using AsyncCourse.Accounting.Api.Models.Issues;

namespace AsyncCourse.Accounting.Api.Domain.Repositories.Issues;

public interface IIssueRepository
{
    Task<List<AccountingIssue>> GetListAsync(List<Guid> ids = null);
    
    Task<AccountingIssue> GetAsync(Guid id);
    
    Task AddAsync(AccountingIssue issue);
    
    Task AddBatchAsync(IEnumerable<AccountingIssue> issues);

    Task<AccountingIssue> UpdateAsync(AccountingIssue issue);

    Task DeleteAsync(Guid id);
}