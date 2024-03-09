using AsyncCourse.Client;
using AsyncCourse.Issues.Api.Models.Accounts;

namespace AsyncCourse.Issues.Api.Client;

public interface IIssuesApiClient
{
    Task<OperationResult<bool>> SaveAccountAsync(IssueAccount account);
    Task<OperationResult<bool>> UpdateAccountAsync(IssueAccount account);
}