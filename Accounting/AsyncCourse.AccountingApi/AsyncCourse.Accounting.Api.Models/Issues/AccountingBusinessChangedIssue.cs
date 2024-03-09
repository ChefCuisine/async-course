namespace AsyncCourse.Accounting.Api.Models.Issues;

public class AccountingBusinessChangedIssue
{
    public Guid IssueId { get; set; }
    public Guid? AssignedToAccountId { get; set; }
}