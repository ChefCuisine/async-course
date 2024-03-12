using AsyncCourse.Issues.Api.Domain.Repositories.OutboxEvents;

namespace AsyncCourse.Issues.Api.Domain.Commands.OutboxEvents;

public interface IRemoveIssueOutboxEventCommand
{
    Task Remove(Guid id);
}

public class RemoveIssueOutboxEventIssueOutboxEventCommand : IRemoveIssueOutboxEventCommand
{
    private readonly IIssueOutboxEventRepository issueOutboxEventRepository;

    public RemoveIssueOutboxEventIssueOutboxEventCommand(IIssueOutboxEventRepository issueOutboxEventRepository)
    {
        this.issueOutboxEventRepository = issueOutboxEventRepository;
    }

    public async Task Remove(Guid id)
    {
        await issueOutboxEventRepository.DeleteAsync(id);
    }
}