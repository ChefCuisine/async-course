using AsyncCourse.Client;
using AsyncCourse.Issues.Api.Models.Accounts;
using AsyncCourse.Issues.Api.Models.OutboxEvents;

namespace AsyncCourse.Issues.Api.Client;

public interface IIssuesApiClient
{
    Task<OperationResult<bool>> SaveAccountAsync(IssueAccount account);
    Task<OperationResult<bool>> UpdateAccountAsync(IssueAccount account);
    
    Task<OperationResult<IssueOutboxEvent>> ReadIssueEventAsync();
    Task<OperationResult<bool>> DeleteIssueEventAsync(Guid id);
}