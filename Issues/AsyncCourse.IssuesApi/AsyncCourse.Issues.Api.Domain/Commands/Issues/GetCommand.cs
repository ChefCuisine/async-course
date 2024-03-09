using AsyncCourse.Issues.Api.Domain.Repositories.Issues;
using AsyncCourse.Issues.Api.Models.Issues;

namespace AsyncCourse.Issues.Api.Domain.Commands.Issues;

public interface IGetCommand
{
    Task<Issue> GetAsync(Guid id);
}

public class GetCommand : IGetCommand
{
    private readonly IIssueRepository issueRepository;

    public GetCommand(IIssueRepository issueRepository)
    {
        this.issueRepository = issueRepository;
    }

    public async Task<Issue> GetAsync(Guid id)
    {
        var issue = await issueRepository.GetAsync(id);

        if (issue != null)
        {
            return issue;
        }

        return null;
    }
}