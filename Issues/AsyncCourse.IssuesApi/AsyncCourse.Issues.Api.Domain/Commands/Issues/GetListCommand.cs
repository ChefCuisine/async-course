using AsyncCourse.Issues.Api.Domain.Repositories;
using AsyncCourse.Issues.Api.Models.Issues;

namespace AsyncCourse.Issues.Api.Domain.Commands.Issues;

public interface IGetListCommand
{
    Task<List<Issue>> GetListAsync();
}

public class GetListCommand : IGetListCommand
{
    private readonly IIssueRepository issueRepository;

    public GetListCommand(IIssueRepository issueRepository)
    {
        this.issueRepository = issueRepository;
    }
    
    public async Task<List<Issue>> GetListAsync()
    {
        var result = await issueRepository.GetListAsync();

        return result;
    }
}