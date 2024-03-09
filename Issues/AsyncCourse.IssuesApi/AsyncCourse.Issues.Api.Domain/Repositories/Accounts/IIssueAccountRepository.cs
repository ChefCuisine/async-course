using AsyncCourse.Issues.Api.Models.Accounts;

namespace AsyncCourse.Issues.Api.Domain.Repositories.Accounts;

public interface IIssueAccountRepository
{
    Task CreateAsync(IssueAccount issueAccount);

    Task UpdateAsync(IssueAccount issueAccount);

    Task<List<IssueAccount>> GetAccountsAsync(IEnumerable<Guid> accountIds = null);
}