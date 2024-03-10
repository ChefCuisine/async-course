using AsyncCourse.Accounting.Api.Domain.Commands.Transactions.Creator;
using AsyncCourse.Accounting.Api.Domain.Repositories.Issues;
using AsyncCourse.Accounting.Api.Models.Issues;
using AsyncCourse.Accounting.Api.Models.Transactions;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Issues;

public interface IReassignIssueCommand
{
    Task ReassignAsync(AccountingBusinessChangedIssue changedIssue);
}

public class ReassignIssueCommand : IReassignIssueCommand
{
    private readonly IIssueRepository issueRepository;
    private readonly ITransactionsCreator transactionsCreator;

    public ReassignIssueCommand(IIssueRepository issueRepository, ITransactionsCreator transactionsCreator)
    {
        this.issueRepository = issueRepository;
        this.transactionsCreator = transactionsCreator;
    }

    public async Task ReassignAsync(AccountingBusinessChangedIssue changedIssue)
    {
        var issue = await issueRepository.GetAsync(changedIssue.IssueId);

        if (issue.AssignedToAccountId == changedIssue.AssignedToAccountId)
        {
            return;
        }

        issue.AssignedToAccountId = changedIssue.AssignedToAccountId;

        await issueRepository.UpdateAsync(issue);

        await transactionsCreator.CreateAsync(
            TransactionType.IssueAssigned,
            issue.IssueId,
            issue.AssignedToAccountId);
    }
}