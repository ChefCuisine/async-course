namespace AsyncCourse.Auth.Api.Models.Models;

public class LoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool KeepLoggedIn { get; set; }
}