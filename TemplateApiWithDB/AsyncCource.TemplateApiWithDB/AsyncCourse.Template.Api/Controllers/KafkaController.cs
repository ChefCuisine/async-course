using AsyncCourse.Template.Api.Domain.Commands.Kafka.Create;
using AsyncCourse.Template.Api.Domain.Commands.Kafka.Get;
using AsyncCourse.Template.Api.Domain.Models.Kafka.Create;
using AsyncCourse.Template.Api.Domain.Models.Kafka.Get;
using Microsoft.AspNetCore.Mvc;

namespace AsyncCource.TemplateApiWithDB.Controllers;

[Route("custom-kafka")]
public class KafkaController : Controller
{
    private readonly ICreateKafkaMessageCommand createKafkaMessageCommand;
    private readonly IGetKafkaMessageCommand getKafkaMessageCommand;

    public KafkaController(
        ICreateKafkaMessageCommand createKafkaMessageCommand,
        IGetKafkaMessageCommand getKafkaMessageCommand)
    {
        this.createKafkaMessageCommand = createKafkaMessageCommand;
        this.getKafkaMessageCommand = getKafkaMessageCommand;
    }

    [HttpGet("test")]
    public int Test()
    {
        return 5;
    }

    [HttpPost("test-message")]
    public async Task<TemplateApiKafkaGetResponse> GetKafkaResponse([FromBody] TemplateApiKafkaCreateRequest request)
    {
        await createKafkaMessageCommand.CreateAsync(request);

        await Task.Delay(3000);

        var result = await getKafkaMessageCommand.StartAsync(new CancellationToken());

        return result;
    }
}