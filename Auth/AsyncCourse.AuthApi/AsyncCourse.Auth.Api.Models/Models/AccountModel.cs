namespace AsyncCourse.Auth.Api.Models.Models;

public class AccountModel
{
    public Guid Id { get; set; }
    
    public string Email { get; set; }

    public string FullName { get; set; }

    public AuthAccountRoleModel Role { get; set; }
}