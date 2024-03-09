using AsyncCourse.Accounting.Api.Models.Accounts;
using AsyncCourse.Accounting.Api.Models.Issues;
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

    public async Task<OperationResult<bool>> SaveAccountAsync(AccountingAccount account)
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

    public async Task<OperationResult<bool>> UpdateAccountAsync(AccountingAccount account)
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

    public async Task<OperationResult<bool>> SaveIssueAsync(AccountingIssue issue)
    {
        var request = Request
            .Post("/accounting-issue/save")
            .WithJsonContent(issue);

        var operationResult = await SendRequestAsync<bool>(request);
        if (operationResult.IsSuccessful)
        {
            return operationResult;
        }
        return operationResult;
    }

    public async Task<OperationResult<bool>> ReassignIssueAsync(AccountingBusinessChangedIssue issue)
    {
        var request = Request
            .Post("/accounting-issue/reassign")
            .WithJsonContent(issue);

        var operationResult = await SendRequestAsync<bool>(request);
        if (operationResult.IsSuccessful)
        {
            return operationResult;
        }
        return operationResult;
    }
    
    public async Task<OperationResult<bool>> CloseIssueAsync(AccountingBusinessChangedIssue issue)
    {
        var request = Request
            .Post("/accounting-issue/close")
            .WithJsonContent(issue);

        var operationResult = await SendRequestAsync<bool>(request);
        if (operationResult.IsSuccessful)
        {
            return operationResult;
        }
        return operationResult;
    }
}