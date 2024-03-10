using AsyncCourse.Issues.Api.Domain.Repositories.Issues;
using AsyncCourse.Issues.Api.Domain.Repositories.OutboxEvents;
using AsyncCourse.Issues.Api.Models.Issues;
using AsyncCourse.Issues.Api.Models.OutboxEvents;

namespace AsyncCourse.Issues.Api.Domain.Commands.Issues;

public interface IDoneCommand
{
    Task DoneAsync(Issue issue);
}

public class DoneCommand : IDoneCommand // todo Role Employee
{
    private readonly IIssueRepository issueRepository;
    private readonly IIssueOutboxEventRepository issueOutboxEventRepository;
    
    public DoneCommand(IIssueRepository issueRepository, IIssueOutboxEventRepository issueOutboxEventRepository)
    {
        this.issueRepository = issueRepository;
        this.issueOutboxEventRepository = issueOutboxEventRepository;
    }
    
    public async Task DoneAsync(Issue issue)
    {
        await issueRepository.UpdateAsync(issue);
        
        var issueEvent = new IssueOutboxEvent
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.Now,
            Type = IssueOutboxEventType.Done,
            IssueId = issue.Id,
            Title = issue.Title,
            Description = issue.Description,
            IssueStatus = issue.Status.ToString(),
            AssignedToAccountId = issue.AssignedToAccountId
        };
        await issueOutboxEventRepository.AddAsync(issueEvent);
    }
}