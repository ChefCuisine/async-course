using AsyncCourse.Accounting.Api.Domain.Commands.Transactions.Creator;
using AsyncCourse.Accounting.Api.Domain.Repositories.Issues;
using AsyncCourse.Accounting.Api.Models.Issues;
using AsyncCourse.Accounting.Api.Models.Transactions;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Issues;

public interface IAddIssueCommand
{
    Task AddAsync(AccountingIssue issue);
}

public class AddIssueCommand : IAddIssueCommand
{
    private readonly IIssueRepository issueRepository;
    private readonly ITransactionsCreator transactionsCreator;

    public AddIssueCommand(IIssueRepository issueRepository, ITransactionsCreator transactionsCreator)
    {
        this.issueRepository = issueRepository;
        this.transactionsCreator = transactionsCreator;
    }

    public async Task AddAsync(AccountingIssue issue)
    {
        await issueRepository.AddAsync(issue);

        await transactionsCreator.CreateAsync(
            TransactionType.IssueAssigned,
            issue.IssueId,
            issue.AssignedToAccountId);
    }
}