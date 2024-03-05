namespace AsyncCourse.Issues.Api.Models.Accounts;

public class IssueAccount
{
    public Guid AccountId { get; set; }
    
    public string Email { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public IssueAccountRole Role { get; set; }
}