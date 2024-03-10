using AsyncCourse.Issues.Api.Domain.Commands.Issues.Assigner;
using AsyncCourse.Issues.Api.Domain.Repositories.Issues;
using AsyncCourse.Issues.Api.Domain.Repositories.OutboxEvents;
using AsyncCourse.Issues.Api.Models.Issues;
using AsyncCourse.Issues.Api.Models.OutboxEvents;

namespace AsyncCourse.Issues.Api.Domain.Commands.Issues;

public interface IReassignCommand
{
    Task Reassign();
}

public class ReassignCommand : IReassignCommand // todo Role Manager
{
    private readonly IIssueAssigner issueAssigner;
    private readonly IIssueRepository issueRepository;
    private readonly IIssueOutboxEventRepository issueOutboxEventRepository;

    public ReassignCommand(
        IIssueAssigner issueAssigner,
        IIssueRepository issueRepository,
        IIssueOutboxEventRepository issueOutboxEventRepository)
    {
        this.issueAssigner = issueAssigner;
        this.issueRepository = issueRepository;
        this.issueOutboxEventRepository = issueOutboxEventRepository;
    }

    public async Task Reassign()
    {
        var issues = (await issueAssigner.AssignAllAsync()).ToList();
        if (!issues.Any())
        {
            return;
        }

        await issueRepository.UpdateBatchAsync(issues);

        await issueOutboxEventRepository.AddBatchAsync(issues.Select(MapToReassignEvent));
    }

    private static IssueOutboxEvent MapToReassignEvent(Issue issue)
    {
        return IssueOutboxEventCreator.Create(issue, IssueOutboxEventType.Reassigned);
    }
}