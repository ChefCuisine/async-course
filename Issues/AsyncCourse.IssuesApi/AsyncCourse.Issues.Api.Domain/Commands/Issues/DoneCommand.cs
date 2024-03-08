using AsyncCourse.Issues.Api.Domain.Commands.Issues.Extensions;
using AsyncCourse.Issues.Api.Domain.Repositories;
using AsyncCourse.Issues.Api.Models.Issues;
using AsyncCourse.Template.Kafka.MessageBus;

namespace AsyncCourse.Issues.Api.Domain.Commands.Issues;

public interface IDoneCommand
{
    Task DoneAsync(Issue issue);
}

public class DoneCommand : IDoneCommand // todo Role Employee
{
    private readonly IIssueRepository issueRepository;
    private readonly ITemlateKafkaMessageBus messageBus;
    
    public DoneCommand(IIssueRepository issueRepository, ITemlateKafkaMessageBus messageBus)
    {
        this.issueRepository = issueRepository;
        this.messageBus = messageBus;
    }
    
    public async Task DoneAsync(Issue issue)
    {
        await issueRepository.Update(issue);

        await SendEvents(issue);
    }
    
    private async Task SendEvents(Issue issue)
    {
        var streamEventMessage = issue
            .GetEventDeleted()
            .ToStreamMessage();

        var businessEventMessage = issue
            .GetEventIssueDone()
            .ToBusinessMessage();

        await messageBus.SendMessageAsync(Constants.IssuesStreamTopic, streamEventMessage);
        await messageBus.SendMessageAsync(Constants.IssuesTopic, businessEventMessage);
    }
}