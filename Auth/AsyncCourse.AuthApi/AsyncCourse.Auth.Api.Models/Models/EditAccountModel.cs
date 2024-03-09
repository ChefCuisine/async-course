namespace AsyncCourse.Auth.Api.Models.Models;

public class EditAccountModel
{
    public Guid Id { get; set; }

    public AuthAccountRoleModel Role { get; set; }
}