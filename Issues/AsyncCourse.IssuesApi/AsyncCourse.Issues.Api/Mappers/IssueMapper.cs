using AsyncCourse.Issues.Api.Models;
using AsyncCourse.Issues.Api.Models.Issues;

namespace AsyncCourse.Issues.Api.Mappers;

public static class IssueMapper
{
    public static IssueModel MapFrom(Issue issue)
    {
        return new IssueModel
        {
            Id = issue.Id,
            Title = issue.Title,
            Description = issue.Description,
            Status = MapToModel(issue.Status),
            AssignedToAccountId = issue.AssignedToAccountId
        };
    }

    public static Issue MapFromIssueModel(IssueModel issueModel)
    {
        return new Issue
        {
            Id = issueModel.Id == Guid.Empty ? Guid.NewGuid() : issueModel.Id,
            Title = issueModel.Title,
            Description = issueModel.Description,
            Status = MapFromModel(issueModel.Status),
            AssignedToAccountId = issueModel.AssignedToAccountId
        };
    }
    
    public static Issue MapFromEditIssueModel(EditIssueModel issueModel)
    {
        return new Issue
        {
            Id = issueModel.Id == Guid.Empty ? Guid.NewGuid() : issueModel.Id,
            Title = issueModel.Title,
            Description = issueModel.Description,
            Status = MapFromModel(issueModel.Status),
            AssignedToAccountId = issueModel.AssignedToAccountId
        };
    }

    private static IssueStatusModel MapToModel(IssueStatus status)
    {
        return status switch
        {
            IssueStatus.Unknown => IssueStatusModel.Unknown,
            IssueStatus.Done => IssueStatusModel.Done,
            IssueStatus.Created => IssueStatusModel.Created,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
    
    private static IssueStatus MapFromModel(IssueStatusModel status)
    {
        return status switch
        {
            IssueStatusModel.Unknown => IssueStatus.Unknown,
            IssueStatusModel.Done => IssueStatus.Done,
            IssueStatusModel.Created => IssueStatus.Created,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}