using System.ComponentModel.DataAnnotations.Schema;
using AsyncCourse.Issues.Api.Models.Accounts;

namespace AsyncCourse.Issues.Api.Db.Dbos;

[Table("accounts")]
public class IssueAccountDbo
{
    [Column("account_id"), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid AccountId { get; set; }
    
    [Column("email")]
    public string Email { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("surname")]
    public string Surname { get; set; }

    [Column("role")]
    public IssueAccountRole Role { get; set; }
}