using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;

namespace AsyncCourse.Client;

public abstract class ClientBase
{
    protected IClusterClient ClusterClient;

    protected ClientBase(IClusterClient clusterClient)
    {
        ClusterClient = clusterClient;
    }

    protected ClientBase()
    {
    }

    protected async Task<OperationResult<T>> SendRequestAsync<T>(
        Request request,
        TimeSpan? timeout = null,
        RequestParameters requestParameters = null)
    {
        using var clusterResult = await SendAsync(request, timeout, requestParameters).ConfigureAwait(false);

        if (!clusterResult.Response.IsSuccessful)
        {
            return OperationResult<T>.Error(clusterResult.Response.Code);
        }

        var result = RequestHelpers.Deserialize<T>(clusterResult.Response.Content.ToString());
        return OperationResult<T>.Success(result);
    }

    private Task<ClusterResult> SendAsync(
        Request request,
        TimeSpan? timeout = null,
        RequestParameters requestParameters = null)
    {
        return ClusterClient.SendAsync(request, requestParameters, timeout);
    }
}