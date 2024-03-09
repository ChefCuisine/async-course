using AsyncCourse.Issues.Api.Domain.Repositories;
using AsyncCourse.Issues.Api.Domain.Repositories.Accounts;
using AsyncCourse.Issues.Api.Domain.Repositories.Issues;
using AsyncCourse.Issues.Api.Models.Accounts;
using AsyncCourse.Issues.Api.Models.Issues;

namespace AsyncCourse.Issues.Api.Domain.Commands.Issues.Assigner;

public interface IIssueAssigner
{
    Task<IEnumerable<Issue>> AssignAllAsync();
    Task<Issue> AssignAsync(Issue issue);
}

public class IssueAssigner : IIssueAssigner
{
    private readonly IIssueRepository issueRepository;
    private readonly IIssueAccountRepository issueAccountRepository;

    private readonly Random random;

    public IssueAssigner(
        IIssueRepository issueRepository,
        IIssueAccountRepository issueAccountRepository)
    {
        this.issueRepository = issueRepository;
        this.issueAccountRepository = issueAccountRepository;

        random = new Random();
    }

    public async Task<IEnumerable<Issue>> AssignAllAsync()
    {
        var allIssues = await issueRepository.GetListAsync();
        var issuesToAssign = allIssues.Where(x => x.Status != IssueStatus.Done).ToList();

        if (!issuesToAssign.Any())
        {
            return new List<Issue>();
        }

        var allAccounts = await issueAccountRepository.GetAccountsAsync();
        var allEmployees = allAccounts.Where(x => x.Role == IssueAccountRole.Employee).ToList();
        
        foreach (var issue in issuesToAssign)
        {
            var employeeIndex = random.Next(0, allEmployees.Count);
            var employee = allEmployees.ElementAt(employeeIndex);
            issue.AssignedToAccountId = employee.AccountId;
        }

        return issuesToAssign;
    }

    public async Task<Issue> AssignAsync(Issue issue)
    {
        var allAccounts = await issueAccountRepository.GetAccountsAsync();
        var allEmployees = allAccounts.Where(x => x.Role == IssueAccountRole.Employee).ToList();

        var employeeIndex = random.Next(0, allEmployees.Count);
        var employee = allEmployees.ElementAt(employeeIndex);
        issue.AssignedToAccountId = employee.AccountId;

        return issue;
    }
}