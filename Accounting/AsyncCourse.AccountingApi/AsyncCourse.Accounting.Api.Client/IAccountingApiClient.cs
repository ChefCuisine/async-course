using AsyncCourse.Accounting.Api.Models.Accounts;
using AsyncCourse.Client;

namespace AsyncCourse.Accounting.Api.Client;

public interface IAccountingApiClient
{
    Task<OperationResult<bool>> SaveAsync(AccountingAccount account);
    Task<OperationResult<bool>> UpdateAsync(AccountingAccount account);
}