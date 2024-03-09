using AsyncCourse.Issues.Api.Domain.Repositories.Accounts;
using AsyncCourse.Issues.Api.Models.Accounts;

namespace AsyncCourse.Issues.Api.Domain.Commands.IssuesAccounts;

public interface IUpdateIssueAccountCommand
{
    Task UpdateAsync(IssueAccount issueAccount);
}

public class UpdateIssueAccountCommand : IUpdateIssueAccountCommand
{
    private readonly IIssueAccountRepository issueAccountRepository;

    public UpdateIssueAccountCommand(IIssueAccountRepository issueAccountRepository)
    {
        this.issueAccountRepository = issueAccountRepository;
    }
    
    public async Task UpdateAsync(IssueAccount issueAccount)
    {
        await issueAccountRepository.UpdateAsync(issueAccount);
    }
}