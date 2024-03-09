using AsyncCourse.Accounting.Api.Domain.Repositories.Issues;
using AsyncCourse.Accounting.Api.Models.Issues;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Issues;

public interface ICloseIssueCommand
{
    Task CloseAsync(AccountingBusinessChangedIssue changedIssue);
}

public class CloseIssueCommand : ICloseIssueCommand
{
    private readonly IIssueRepository issueRepository;

    public CloseIssueCommand(IIssueRepository issueRepository)
    {
        this.issueRepository = issueRepository;
    }

    public async Task CloseAsync(AccountingBusinessChangedIssue changedIssue)
    {
        throw new NotImplementedException();
    }
}