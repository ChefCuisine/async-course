namespace AsyncCourse.Accounting.Api.Models.Issues;

public class AccountingIssue
{
    public Guid IssueId { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }

    public AccountingIssueStatus Status { get; set; }

    public Guid? AssignedToAccountId { get; set; }
}