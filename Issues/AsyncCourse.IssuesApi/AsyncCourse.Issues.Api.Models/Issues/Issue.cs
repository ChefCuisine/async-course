namespace AsyncCourse.Issues.Api.Models.Issues;

public class Issue
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }

    public IssueStatus Status { get; set; }
    
    public Guid? AccountId { get; set; }
}