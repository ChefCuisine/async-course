using AsyncCourse.Accounting.Api.Models.Accounts;
using AsyncCourse.Client;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Logging.Abstractions;

namespace AsyncCourse.Accounting.Api.Client;

public class AccountingApiClient : RootClientBase, IAccountingApiClient
{
    public AccountingApiClient(Uri uri, ILog log) : base(uri, log)
    {
    }

    public AccountingApiClient(ClusterClientSetup setup, ILog log) : base(setup, log)
    {
    }

    public async Task<OperationResult<bool>> SaveAsync(AccountingAccount account)
    {
        var request = Request
            .Post("/accounting-account/save")
            .WithJsonContent(account);

        var operationResult = await SendRequestAsync<bool>(request);
        if (operationResult.IsSuccessful)
        {
            return operationResult;
        }
        return operationResult;
    }

    public async Task<OperationResult<bool>> UpdateAsync(AccountingAccount account)
    {
        var request = Request
            .Post("/accounting-account/update")
            .WithJsonContent(account);

        var operationResult = await SendRequestAsync<bool>(request);
        if (operationResult.IsSuccessful)
        {
            return operationResult;
        }
        return operationResult;
    }
}