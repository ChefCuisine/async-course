namespace AsyncCourse.Accounting.Api.Models.Accounts;

public class AccountingAccount
{
    public Guid AccountId { get; set; }
    
    public string Email { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public AccountingAccountRole Role { get; set; }
}