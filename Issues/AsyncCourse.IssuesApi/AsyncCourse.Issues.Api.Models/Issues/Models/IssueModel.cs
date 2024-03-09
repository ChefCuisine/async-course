namespace AsyncCourse.Issues.Api.Models.Issues.Models;

public class IssueModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    
    public string Description { get; set; }

    public IssueStatusModel Status { get; set; }

    public Guid? AssignedToAccountId { get; set; }
}