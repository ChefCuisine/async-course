namespace AsyncCourse.Auth.Api.Models.Models;

public class SignupModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string RepeatedPassword { get; set; }
    public string Surname { get; set; }
    public string Name { get; set; }
}