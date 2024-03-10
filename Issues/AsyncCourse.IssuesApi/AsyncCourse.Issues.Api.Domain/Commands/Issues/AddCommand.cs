using AsyncCourse.Issues.Api.Domain.Commands.Issues.Assigner;
using AsyncCourse.Issues.Api.Domain.Repositories.Issues;
using AsyncCourse.Issues.Api.Domain.Repositories.OutboxEvents;
using AsyncCourse.Issues.Api.Models.Issues;
using AsyncCourse.Issues.Api.Models.OutboxEvents;

namespace AsyncCourse.Issues.Api.Domain.Commands.Issues;

public interface IAddCommand
{
    Task AddAsync(Issue issue);
}

public class AddCommand : IAddCommand // todo Role Any
{
    private readonly IIssueAssigner issueAssigner;
    private readonly IIssueRepository issueRepository;
    private readonly IIssueOutboxEventRepository issueOutboxEventRepository;

    public AddCommand(
        IIssueAssigner issueAssigner,
        IIssueRepository issueRepository,
        IIssueOutboxEventRepository issueOutboxEventRepository)
    {
        this.issueAssigner = issueAssigner;
        this.issueRepository = issueRepository;
        this.issueOutboxEventRepository = issueOutboxEventRepository;
    }

    public async Task AddAsync(Issue issue)
    {
        var assignedIssue = await issueAssigner.AssignAsync(issue);
        
        await issueRepository.AddAsync(assignedIssue);

        var issueEvent = new IssueOutboxEvent
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            Type = IssueOutboxEventType.Created,
            IssueId = assignedIssue.Id,
            Title = assignedIssue.Title,
            Description = assignedIssue.Description,
            IssueStatus = assignedIssue.Status.ToString(),
            AssignedToAccountId = assignedIssue.AssignedToAccountId
        };
        await issueOutboxEventRepository.AddAsync(issueEvent);
    }
}