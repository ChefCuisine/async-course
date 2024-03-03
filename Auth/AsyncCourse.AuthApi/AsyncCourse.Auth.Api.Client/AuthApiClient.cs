using AsyncCourse.Client;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Logging.Abstractions;

namespace AsyncCourse.Auth.Api.Client;

public class AuthApiClient : RootClientBase, IAuthApiClient
{
    public AuthApiClient(Uri uri, ILog log) : base(uri, log)
    {
    }

    public AuthApiClient(ClusterClientSetup setup, ILog log) : base(setup, log)
    {
    }

    public async Task<OperationResult<bool>> Authenticated(string cookie)
    {
        var request = Request
            .Get("/external-access/login")
            .WithHeader("Cookie", cookie);

        var operationResult = await SendRequestAsync<bool>(request);
        if (operationResult.IsSuccessful)
        {
            return operationResult;
        }
        return operationResult;
    }
}