using AsyncCourse.Accounting.Api.Domain.Commands.Transactions.Creator;
using AsyncCourse.Accounting.Api.Domain.Repositories.Issues;
using AsyncCourse.Accounting.Api.Models.Issues;
using AsyncCourse.Accounting.Api.Models.Transactions;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Issues;

public interface ICloseIssueCommand
{
    Task CloseAsync(AccountingBusinessChangedIssue changedIssue);
}

public class CloseIssueCommand : ICloseIssueCommand
{
    private readonly IIssueRepository issueRepository;
    private readonly ITransactionsCreator transactionsCreator;

    public CloseIssueCommand(IIssueRepository issueRepository, ITransactionsCreator transactionsCreator)
    {
        this.issueRepository = issueRepository;
        this.transactionsCreator = transactionsCreator;
    }

    public async Task CloseAsync(AccountingBusinessChangedIssue changedIssue)
    {
        var issue = await issueRepository.GetAsync(changedIssue.IssueId);

        issue.Status = AccountingIssueStatus.Done;

        await issueRepository.UpdateAsync(issue);

        await transactionsCreator.CreateAsync(
            TransactionType.IssueDone,
            issue.IssueId,
            issue.AssignedToAccountId);
    }
}