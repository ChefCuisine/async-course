using AsyncCourse.Issues.Api.Domain.Repositories.OutboxEvents;
using AsyncCourse.Issues.Api.Models.OutboxEvents;

namespace AsyncCourse.Issues.Api.Domain.Commands.OutboxEvents;

public interface IReadOneIssueOutboxEventCommand
{
    Task<IssueOutboxEvent> Read();
}

public class ReadOneIssueOutboxEventIssueOutboxEventCommand : IReadOneIssueOutboxEventCommand
{
    private readonly IIssueOutboxEventRepository issueOutboxEventRepository;

    public ReadOneIssueOutboxEventIssueOutboxEventCommand(IIssueOutboxEventRepository issueOutboxEventRepository)
    {
        this.issueOutboxEventRepository = issueOutboxEventRepository;
    }
    
    public async Task<IssueOutboxEvent> Read()
    {
        return await issueOutboxEventRepository.GetNextAsync();
    }
}