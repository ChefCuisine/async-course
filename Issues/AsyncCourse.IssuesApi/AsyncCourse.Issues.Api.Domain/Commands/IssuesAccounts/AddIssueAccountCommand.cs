using AsyncCourse.Issues.Api.Domain.Repositories;
using AsyncCourse.Issues.Api.Models.Accounts;

namespace AsyncCourse.Issues.Api.Domain.Commands.IssuesAccounts;

public interface IAddIssueAccountCommand
{
    Task AddAsync(IssueAccount issueAccount);
}

public class AddIssueAccountCommand : IAddIssueAccountCommand
{
    private readonly IIssueAccountRepository issueAccountRepository;

    public AddIssueAccountCommand(IIssueAccountRepository issueAccountRepository)
    {
        this.issueAccountRepository = issueAccountRepository;
    }

    public async Task AddAsync(IssueAccount issueAccount)
    {
        await issueAccountRepository.CreateAsync(issueAccount);
    }
}