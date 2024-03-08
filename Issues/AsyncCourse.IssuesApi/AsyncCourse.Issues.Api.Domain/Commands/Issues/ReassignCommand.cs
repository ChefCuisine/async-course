using AsyncCourse.Issues.Api.Domain.Commands.Issues.Assigner;
using AsyncCourse.Issues.Api.Domain.Commands.Issues.Extensions;
using AsyncCourse.Issues.Api.Domain.Repositories;
using AsyncCourse.Issues.Api.Models.Issues;
using AsyncCourse.Template.Kafka.MessageBus;

namespace AsyncCourse.Issues.Api.Domain.Commands.Issues;

public interface IReassignCommand
{
    Task Reassign();
}

public class ReassignCommand : IReassignCommand // todo Role Manager
{
    private readonly IIssueAssigner issueAssigner;
    private readonly IIssueRepository issueRepository;
    private readonly ITemlateKafkaMessageBus messageBus;

    public ReassignCommand(
        IIssueAssigner issueAssigner,
        IIssueRepository issueRepository,
        ITemlateKafkaMessageBus messageBus)
    {
        this.issueAssigner = issueAssigner;
        this.issueRepository = issueRepository;
        this.messageBus = messageBus;
    }

    public async Task Reassign()
    {
        var issues = (await issueAssigner.AssignAllAsync()).ToList();
        if (!issues.Any())
        {
            return;
        }

        await issueRepository.AddBatchAsync(issues);

        foreach (var assignedIssue in issues) // todo toBatch
        {
            await SendEvents(assignedIssue);
        }
    }

    private async Task SendEvents(Issue assignedIssue)
    {
        var businessEventMessage = assignedIssue
            .GetEventIssueReassigned()
            .ToBusinessMessage();

        await messageBus.SendMessageAsync(Constants.IssuesTopic, businessEventMessage);
    }
}