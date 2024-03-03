using AsyncCourse.Issues.Api.Domain.Repositories;
using AsyncCourse.Issues.Api.Models.Issues;

namespace AsyncCourse.Issues.Api.Domain.Commands.Issues;

public interface IAddCommand
{
    Task AddAsync(Issue issue);
}

public class AddCommand : IAddCommand
{
    private readonly IIssueRepository issueRepository;

    public AddCommand(IIssueRepository issueRepository)
    {
        this.issueRepository = issueRepository;
    }

    public async Task AddAsync(Issue issue)
    {
        await issueRepository.AddAsync(issue);
    }
}