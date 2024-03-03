using System.ComponentModel.DataAnnotations.Schema;
using AsyncCourse.Auth.Api.Models.Accounts;

namespace AsyncCourse.Auth.Api.Db.Dbos;

[Table("accounts")]
public class AuthAccountDbo
{
    [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
    
    [Column("email")]
    public string Email { get; set; }

    [Column("password")]
    public string Password { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("surname")]
    public string Surname { get; set; }
    
    [Column("Role")]
    public AuthAccountRole Role { get; set; }
}