using AsyncCourse.Accounting.Api.Domain.Repositories.Issues;
using AsyncCourse.Accounting.Api.Models.Issues;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Issues;

public interface IReassignIssueCommand
{
    Task ReassingAsync(AccountingBusinessChangedIssue changedIssue);
}

public class ReassignIssueCommand : IReassignIssueCommand
{
    private readonly IIssueRepository issueRepository;

    public ReassignIssueCommand(IIssueRepository issueRepository)
    {
        this.issueRepository = issueRepository;
    }

    public async Task ReassingAsync(AccountingBusinessChangedIssue changedIssue)
    {
        throw new NotImplementedException();
    }
}