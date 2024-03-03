namespace AsyncCourse.Auth.Api.Models.Accounts;

public class AuthAccount
{
    public Guid Id { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public AuthAccountRole Role { get; set; }
}