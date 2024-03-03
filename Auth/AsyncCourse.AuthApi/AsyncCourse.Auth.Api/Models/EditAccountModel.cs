namespace AsyncCourse.Auth.Api.Models;

public class EditAccountModel
{
    public Guid Id { get; set; }

    public AuthAccountRoleModel Role { get; set; }
}