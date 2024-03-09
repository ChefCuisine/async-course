using AsyncCourse.Issues.Api.Models.Issues;
using AsyncCourse.Issues.Api.Models.Issues.Models;

namespace AsyncCourse.Issues.Api.Models.Mappers;

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
    
    public static IssueListModel MapToListModel(Issue issue, Dictionary<Guid, string> accountsMap)
    {
        var fullName = string.Empty;

        if (issue.AssignedToAccountId.HasValue && accountsMap.ContainsKey(issue.AssignedToAccountId.Value))
        {
            fullName = accountsMap[issue.AssignedToAccountId.Value];
        }

        return new IssueListModel
        {
            Id = issue.Id,
            Title = issue.Title,
            Description = issue.Description,
            Status = MapToModel(issue.Status),
            Responsible = fullName
        };
    }
    
    public static Issue MapFromCreateIssueModel(CreateIssueModel issueModel)
    {
        return new Issue
        {
            Id =  Guid.NewGuid(),
            Title = issueModel.Title,
            Description = issueModel.Description,
            Status = IssueStatus.Created
        };
    }

    public static Issue MapFromIssueModel(IssueModel issueModel)
    {
        var status = issueModel.Status == IssueStatusModel.Unknown 
            ? IssueStatusModel.Created
            : issueModel.Status;

        return new Issue
        {
            Id = issueModel.Id == Guid.Empty ? Guid.NewGuid() : issueModel.Id,
            Title = issueModel.Title,
            Description = issueModel.Description,
            Status = MapFromModel(status),
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