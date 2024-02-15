using AsyncCourse.Template.Api.Domain.Models.Kafka.Create;
using AsyncCourse.Template.Kafka.MessageBus;
using Newtonsoft.Json;

namespace AsyncCourse.Template.Api.Domain.Commands.Kafka.Create;

public interface ICreateKafkaMessageCommand
{
    Task CreateAsync(TemplateApiKafkaCreateRequest request);
}

public class CreateKafkaMessageCommand : ICreateKafkaMessageCommand
{
    private readonly ITemlateKafkaMessageBus messageBus;

    public CreateKafkaMessageCommand(ITemlateKafkaMessageBus messageBus)
    {
        this.messageBus = messageBus;
    }

    public async Task CreateAsync(TemplateApiKafkaCreateRequest request)
    {
        var message = JsonConvert.SerializeObject(request);

        await messageBus.SendMessageAsync(Constants.TestTopic, message);
    }
}