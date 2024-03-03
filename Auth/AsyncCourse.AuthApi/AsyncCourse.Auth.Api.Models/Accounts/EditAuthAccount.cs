namespace AsyncCourse.Auth.Api.Models.Accounts;

public class EditAuthAccount
{
    public Guid Id { get; set; }

    public AuthAccountRole Role { get; set; }
}