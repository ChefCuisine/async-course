using AsyncCourse.Client;
using AsyncCourse.Template.Api.Domain.Models.Kafka.Create;
using AsyncCourse.Template.Api.Domain.Models.Kafka.Get;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Logging.Abstractions;

namespace AsyncCourse.Template.Api.Client;

public class TemplateApiClient : RootClientBase, ITemplateApiClient
{
    public TemplateApiClient(Uri uri, ILog log) : base(uri, log)
    {
    }

    public TemplateApiClient(ClusterClientSetup setup, ILog log) : base(setup, log)
    {
    }

    public async Task<OperationResult<int>> Test()
    {
        var request = Request
            .Get("custom-kafka/test");
        var operationResult = await SendRequestAsync<int>(request);
        return operationResult;
    }
    
    public async Task<OperationResult<TemplateApiKafkaGetResponse>> TestMessage(
        TemplateApiKafkaCreateRequest createRequest)
    {
        var request = Request
            .Post("custom-kafka/test-message")
            .WithJsonContent(createRequest);
        var operationResult = await SendRequestAsync<TemplateApiKafkaGetResponse>(request);
        return operationResult;
    }
}