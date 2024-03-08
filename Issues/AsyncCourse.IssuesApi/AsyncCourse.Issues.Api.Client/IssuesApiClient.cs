using AsyncCourse.Client;
using AsyncCourse.Issues.Api.Models.Accounts;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Logging.Abstractions;

namespace AsyncCourse.Issues.Api.Client;

public class IssuesApiClient : RootClientBase, IIssuesApiClient
{
    public IssuesApiClient(Uri uri, ILog log) : base(uri, log)
    {
    }

    public IssuesApiClient(ClusterClientSetup setup, ILog log) : base(setup, log)
    {
    }

    public async Task<OperationResult<bool>> SaveAsync(IssueAccount account)
    {
        var request = Request
            .Post("/issues-account/save")
            .WithJsonContent(account);

        var operationResult = await SendRequestAsync<bool>(request);
        if (operationResult.IsSuccessful)
        {
            return operationResult;
        }
        return operationResult;
    }

    public async Task<OperationResult<bool>> UpdateAsync(IssueAccount account)
    {
        var request = Request
            .Post("/issues-account/update")
            .WithJsonContent(account);

        var operationResult = await SendRequestAsync<bool>(request);
        if (operationResult.IsSuccessful)
        {
            return operationResult;
        }
        return operationResult;
    }
}