using AsyncCourse.Issues.Api.Domain.Repositories;
using AsyncCourse.Issues.Api.Models.Accounts;
using AsyncCourse.Issues.Api.Models.Issues;

namespace AsyncCourse.Issues.Api.Domain.Commands.Issues;

public interface IReassignCommand
{
    Task Reassign();
}

public class ReassignCommand : IReassignCommand
{
    private readonly IIssueRepository issueRepository;
    private readonly IIssueAccountRepository issueAccountRepository;

    private readonly Random random;

    public ReassignCommand(IIssueRepository issueRepository, IIssueAccountRepository issueAccountRepository)
    {
        this.issueRepository = issueRepository;
        this.issueAccountRepository = issueAccountRepository;

        random = new Random();
    }

    public async Task Reassign()
    {
        var allIssues = await issueRepository.GetListAsync();
        var allUnassigned = allIssues.Where(x => x.Status != IssueStatus.Done);

        var allAccounts = await issueAccountRepository.GetAccountsAsync();
        var allEmployees = allAccounts.Where(x => x.Role == IssueAccountRole.Employee).ToList();

        foreach (var issue in allUnassigned)
        {
            var employeeIndex = random.Next(0, allEmployees.Count);
            var employee = allEmployees.ElementAt(employeeIndex);
            issue.AccountId = employee.AccountId;

            await issueRepository.AddAsync(issue);
        }
    }
}