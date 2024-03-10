using AsyncCourse.Client;
using AsyncCourse.Issues.Api.Models.Accounts;
using AsyncCourse.Issues.Api.Models.OutboxEvents;
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

    public async Task<OperationResult<bool>> SaveAccountAsync(IssueAccount account)
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

    public async Task<OperationResult<bool>> UpdateAccountAsync(IssueAccount account)
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

    public async Task<OperationResult<IssueOutboxEvent>> ReadIssueEventAsync()
    {
        var request = Request
            .Get("/issues-event/read-one");

        var operationResult = await SendRequestAsync<IssueOutboxEvent>(request);
        if (operationResult.IsSuccessful)
        {
            return operationResult;
        }
        return operationResult;
    }

    public async Task<OperationResult<bool>> DeleteIssueEventAsync(Guid id)
    {
        var request = Request
            .Delete($"/issues-event/delete/{id}");

        var operationResult = await SendRequestAsync<bool>(request);
        if (operationResult.IsSuccessful)
        {
            return operationResult;
        }
        return operationResult;
    }
}