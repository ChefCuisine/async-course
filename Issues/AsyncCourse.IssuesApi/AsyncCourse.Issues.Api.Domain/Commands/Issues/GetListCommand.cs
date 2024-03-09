using AsyncCourse.Issues.Api.Domain.Repositories.Accounts;
using AsyncCourse.Issues.Api.Domain.Repositories.Issues;
using AsyncCourse.Issues.Api.Models.Issues.Models;
using AsyncCourse.Issues.Api.Models.Mappers;

namespace AsyncCourse.Issues.Api.Domain.Commands.Issues;

public interface IGetListCommand
{
    Task<IEnumerable<IssueListModel>> GetListAsync();
}

public class GetListCommand : IGetListCommand
{
    private readonly IIssueRepository issueRepository;
    private readonly IIssueAccountRepository issueAccountRepository;

    public GetListCommand(
        IIssueRepository issueRepository,
        IIssueAccountRepository issueAccountRepository)
    {
        this.issueRepository = issueRepository;
        this.issueAccountRepository = issueAccountRepository;
    }
    
    public async Task<IEnumerable<IssueListModel>> GetListAsync()
    {
        var issuesResult = await issueRepository.GetListAsync();

        var assignedEmployeeIds = issuesResult
            .Where(x => x.AssignedToAccountId.HasValue)
            .Select(x => x.AssignedToAccountId.Value);

        var accountsResult = await issueAccountRepository.GetAccountsAsync(assignedEmployeeIds);
        var accountsMap = accountsResult.ToDictionary(
            k => k.AccountId,
            v => IssueAccountMapper.GetFullName(v.Surname, v.Name));

        var result = issuesResult
            .Select(issue => IssueMapper.MapToListModel(issue, accountsMap));
        
        return result;
    }
}