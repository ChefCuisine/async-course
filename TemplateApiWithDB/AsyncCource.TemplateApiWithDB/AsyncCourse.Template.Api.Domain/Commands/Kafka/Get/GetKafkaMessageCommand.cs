using AsyncCourse.Template.Api.Domain.Models.Kafka.Create;
using AsyncCourse.Template.Api.Domain.Models.Kafka.Get;
using AsyncCourse.Template.Kafka.MessageBus;

namespace AsyncCourse.Template.Api.Domain.Commands.Kafka.Get;

public interface IGetKafkaMessageCommand
{
    Task<TemplateApiKafkaGetResponse> StartAsync(CancellationToken cancellationToken);
}

public class GetKafkaMessageCommand : IGetKafkaMessageCommand
{
    private readonly ITemlateKafkaMessageBus messageBus;

    public GetKafkaMessageCommand(ITemlateKafkaMessageBus messageBus)
    {
        this.messageBus = messageBus;
    }

    public async Task<TemplateApiKafkaGetResponse> StartAsync(CancellationToken cancellationToken)
    {
        var kafkaResult = messageBus.Consume<TemplateApiKafkaCreateRequest>(Constants.TestTopic, cancellationToken);

        // можно было сразу передать Consume<TemplateApiKafkaGetResponse>
        // но обычно бывает что все равно нужна какая-то обработка, так что будем получать то, что отправили
        var response = new TemplateApiKafkaGetResponse
        {
            OrderId = kafkaResult.OrderId,
            ProductId = kafkaResult.ProductId,
            CustomerId = kafkaResult.CustomerId,
            Quantity = kafkaResult.Quantity,
            Status = "Done"
        };

        return response;
    }
}