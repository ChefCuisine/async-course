using AsyncCourse.Issues.Api.Domain.Commands.Issues.Assigner;
using AsyncCourse.Issues.Api.Domain.Commands.Issues.Extensions;
using AsyncCourse.Issues.Api.Domain.Repositories.Issues;
using AsyncCourse.Issues.Api.Models.Issues;
using AsyncCourse.Template.Kafka.MessageBus;

namespace AsyncCourse.Issues.Api.Domain.Commands.Issues;

public interface IAddCommand
{
    Task AddAsync(Issue issue);
}

public class AddCommand : IAddCommand // todo Role Any
{
    private readonly IIssueAssigner issueAssigner;
    private readonly IIssueRepository issueRepository;
    private readonly ITemlateKafkaMessageBus messageBus;

    public AddCommand(
        IIssueAssigner issueAssigner,
        IIssueRepository issueRepository,
        ITemlateKafkaMessageBus messageBus)
    {
        this.issueAssigner = issueAssigner;
        this.issueRepository = issueRepository;
        this.messageBus = messageBus;
    }

    public async Task AddAsync(Issue issue)
    {
        var assignedIssue = await issueAssigner.AssignAsync(issue);
        
        await issueRepository.AddAsync(assignedIssue);

        await SendEventsAsync(assignedIssue);
    }

    private async Task SendEventsAsync(Issue assignedIssue)
    {
        var streamEventMessage = assignedIssue
            .GetEventCreated()
            .ToStreamMessage();

        await messageBus.SendMessageAsync(Constants.IssuesStreamTopic, streamEventMessage);
    }
}