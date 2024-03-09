namespace AsyncCourse.Issues.Api.Models.Issues.Models;

public class IssueListModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    
    public string Description { get; set; }

    public IssueStatusModel Status { get; set; }
    
    public string Responsible { get; set; }
}