using AsyncCourse.Accounting.Api.Models.Accounts;
using AsyncCourse.Accounting.Api.Models.Issues;
using AsyncCourse.Client;

namespace AsyncCourse.Accounting.Api.Client;

public interface IAccountingApiClient
{
    Task<OperationResult<bool>> SaveAccountAsync(AccountingAccount account);
    Task<OperationResult<bool>> UpdateAccountAsync(AccountingAccount account);
    Task<OperationResult<bool>> SaveIssueAsync(AccountingIssue issue);
    Task<OperationResult<bool>> ReassignIssueAsync(AccountingBusinessChangedIssue issue);
    Task<OperationResult<bool>> CloseIssueAsync(AccountingBusinessChangedIssue issue);
}